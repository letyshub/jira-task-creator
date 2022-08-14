using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class FakeHttpHandler : IHttpHandler
{
    public void Dispose()
    {
    }

    public Task<HttpResponseMessage> GetAsync(string url, CancellationToken ct)
    {
        switch (url)
        {
            case "https://www.test.com":
                return Task.FromResult<HttpResponseMessage>(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("<html><title>Test title</title></html>")
                });
            case "https://www.test-missing-title.com":
                return Task.FromResult<HttpResponseMessage>(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("<html></html>")
                });
            default:
                return Task.FromResult<HttpResponseMessage>(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    ReasonPhrase = "Not found"
                });
        }
    }
}