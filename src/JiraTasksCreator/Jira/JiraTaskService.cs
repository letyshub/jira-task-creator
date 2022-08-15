using System.Text;
using System.Text.Json;

public class JiraTaskService : IJiraTaskService
{
    private readonly JiraConfiguration _configuration;
    private readonly IWebpageRetriever _webpageRetriever;
    private readonly HttpClient _httpClient;

    public JiraTaskService(JiraConfiguration configuration, IHttpClientFactory httpClientFactory, IWebpageRetriever webpageRetriever)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient("Jira");
        _webpageRetriever = webpageRetriever;
    }

    public async Task CreateTaskAsync(string url, CancellationToken ct)
    {
        var webpageTitle = await _webpageRetriever.GetTitleAsync(url, ct);
        var rq = new JiraCreateTaskRequest(url, webpageTitle, _configuration.Project, _configuration.TaskParentId);
        var payload = new StringContent(JsonSerializer.Serialize(rq), Encoding.UTF8, "application/json");
        var rs = await _httpClient.PostAsync(_configuration.Url, payload, ct);

        if (!rs.IsSuccessStatusCode)
        {
            throw new Exception(rs.ReasonPhrase);
        }
    }
}