using System.Text;
using System.Text.Json;

public class JiraTaskCreator : IJiraTaskCreator
{
    private readonly JiraConfiguration _configuration;
    private readonly IWebpageRetriever _webpageRetriever;
    private readonly HttpClient _httpClient;

    public JiraTaskCreator(JiraConfiguration configuration, IHttpClientFactory httpClientFactory, IWebpageRetriever webpageRetriever)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient("Jira");
        _webpageRetriever = webpageRetriever;
    }

    public async Task CreateTaskAsync(string url, CancellationToken ct)
    {
        var webpageTitle = await _webpageRetriever.GetTitleAsync(url, ct);
        var rq = new JiraCreateTaskRequest
        {
            Fields = new JiraFields
            {
                Description = url,
                Summary = webpageTitle,
                Project = new JiraProject { Key = _configuration.Project },
                Parent = new JiraParent { Id = _configuration.TaskParentId }
            }
        };

        var requestContent = new StringContent(JsonSerializer.Serialize(rq), Encoding.UTF8, "application/json");
        var rs = await _httpClient.PostAsync(_configuration.Url, requestContent, ct);

        if (!rs.IsSuccessStatusCode)
        {
            throw new Exception(rs.ReasonPhrase);
        }
    }
}