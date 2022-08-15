using System.Xml;

public class WebpageRetriever : IWebpageRetriever, IDisposable
{
    private readonly HttpClient _httpClient;

    public WebpageRetriever(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public async Task<string> GetTitleAsync(string url, CancellationToken ct)
    {
        var rs = await _httpClient.GetAsync(url, ct);

        if (!rs.IsSuccessStatusCode)
        {
            throw new Exception(rs.ReasonPhrase);
        }

        var body = await rs.Content.ReadAsStringAsync(ct);
        var titleStartTag = body.IndexOf("<title>", StringComparison.OrdinalIgnoreCase);
        var titleEndTag = body.IndexOf("</title>", StringComparison.OrdinalIgnoreCase);

        if (titleStartTag == -1 || titleEndTag == -1)
        {
            throw new Exception("Missing webpage's title");
        }

        return body.Substring(titleStartTag + 7, titleEndTag - titleStartTag - 7);
    }
}