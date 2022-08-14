using System.Xml;

public class WebpageRetriever : IWebpageRetriever, IDisposable
{
    private readonly IHttpHandler _httpHandler;

    public WebpageRetriever(IHttpHandler httpHandler)
    {
        _httpHandler = httpHandler;
    }

    public void Dispose()
    {
        _httpHandler.Dispose();
    }

    public async Task<string> GetTitleAsync(string url, CancellationToken ct)
    {
        var rs = await _httpHandler.GetAsync(url, ct);

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