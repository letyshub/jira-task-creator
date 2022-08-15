using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class FakeHttpHandler : DelegatingHandler
{
    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
    public FakeHttpHandler()
    {
        _handlerFunc = (request, ct) =>
        {
            if (request.RequestUri == null)
            {
                throw new Exception("Empty url");
            }

            switch (request.Method.ToString().ToUpper())
            {
                case "GET":
                    return GetAsync(request.RequestUri.ToString(), ct);
                case "POST":
                    return PostAsync(request.RequestUri.ToString(), ct);
                default:
                    throw new Exception("Not supported");
            }
        };
    }

    public FakeHttpHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
    {
        _handlerFunc = handlerFunc;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _handlerFunc(request, cancellationToken);
    }

    private Task<HttpResponseMessage> GetAsync(string url, CancellationToken ct)
    {
        switch (url)
        {
            case "https://www.test.com/":
                return Task.FromResult<HttpResponseMessage>(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("<html><title>Test title</title></html>")
                });
            case "https://www.test-missing-title.com/":
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

    private Task<HttpResponseMessage> PostAsync(string url, CancellationToken ct)
    {
        switch (url)
        {
            case "https://www.test.com/":
                return Task.FromResult<HttpResponseMessage>(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK
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