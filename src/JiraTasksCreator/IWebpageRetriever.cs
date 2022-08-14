public interface IWebpageRetriever
{
    Task<string> GetTitleAsync(string url, CancellationToken ct);
}