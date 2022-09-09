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
        var mockHttpClientFactory = new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        _httpClientFactory = mockHttpClientFactory.Object;
    }

    [Fact]
    public async Task Should_Retrieve_Webpage_Title()
    {
        var mockTextParserService = new Mock<ITextParserService>();
        mockTextParserService.Setup(_ => _.GetHtmlTitle(It.IsAny<string>())).Returns("Test title");
        var retriever = new WebpageRetriever(_httpClientFactory, mockTextParserService.Object);
        var actual = await retriever.GetTitleAsync("https://www.test.com", System.Threading.CancellationToken.None);
        Assert.Equal("Test title", actual);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Webpage_NotFound()
    {
        var mockTextParserService = new Mock<ITextParserService>();
        var retriever = new WebpageRetriever(_httpClientFactory, mockTextParserService.Object);
        var actual = await Assert.ThrowsAsync<Exception>(() => retriever.GetTitleAsync("https://www.notfound.com", System.Threading.CancellationToken.None));
        Assert.Equal("Not found", actual.Message);
    }
}