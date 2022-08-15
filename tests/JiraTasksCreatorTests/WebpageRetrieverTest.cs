using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Xunit;

public class WebpageRetrieverTest
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WebpageRetrieverTest()
    {
        var clientHandlerStub = new FakeHttpHandler();
        var client = new HttpClient(clientHandlerStub);
        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        _httpClientFactory = mockFactory.Object;
    }

    [Fact]
    public async Task Should_Retrieve_Webpage_Title()
    {
        var retriever = new WebpageRetriever(_httpClientFactory);
        var actual = await retriever.GetTitleAsync("https://www.test.com", System.Threading.CancellationToken.None);
        Assert.Equal("Test title", actual);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Webpage_NotFound()
    {
        var retriever = new WebpageRetriever(_httpClientFactory);
        var actual = await Assert.ThrowsAsync<Exception>(() => retriever.GetTitleAsync("https://www.notfound.com", System.Threading.CancellationToken.None));
        Assert.Equal("Not found", actual.Message);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Webpage_NotContains_Title()
    {
        var retriever = new WebpageRetriever(_httpClientFactory);
        var actual = await Assert.ThrowsAsync<Exception>(() => retriever.GetTitleAsync("https://www.test-missing-title.com", System.Threading.CancellationToken.None));
        Assert.Equal("Missing webpage's title", actual.Message);
    }
}