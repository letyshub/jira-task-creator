public class HttpHandler : IHttpHandler
{
    private readonly HttpClient _httpClient;

    public HttpHandler()
    {
        _httpClient = new HttpClient();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public Task<HttpResponseMessage> GetAsync(string url, CancellationToken ct)
    {
        return _httpClient.GetAsync(url, ct);
    }
}