public interface IHttpHandler : IDisposable
{
    Task<HttpResponseMessage> GetAsync(string url, CancellationToken ct);
}