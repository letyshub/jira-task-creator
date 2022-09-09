public class WebpageRetriever : IWebpageRetriever, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ITextParserService _parser;

    public WebpageRetriever(IHttpClientFactory httpClientFactory, ITextParserService parser)
    {
        _httpClient = httpClientFactory.CreateClient("Default");
        _parser = parser;
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
        return _parser.GetHtmlTitle(body);
    }
}