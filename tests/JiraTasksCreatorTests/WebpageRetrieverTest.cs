using System;
using System.Threading.Tasks;
using Xunit;

public class WebpageRetrieverTest
{

    [Fact]
    public async Task Should_Retrieve_Webpage_Title()
    {
        var retriever = new WebpageRetriever(new FakeHttpHandler());
        var actual = await retriever.GetTitleAsync("https://www.test.com", System.Threading.CancellationToken.None);
        Assert.Equal("Test title", actual);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Webpage_NotFound()
    {
        var retriever = new WebpageRetriever(new FakeHttpHandler());
        var actual = await Assert.ThrowsAsync<Exception>(() => retriever.GetTitleAsync("https://www.notfound.com", System.Threading.CancellationToken.None));
        Assert.Equal("Not found", actual.Message);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Webpage_NotContains_Title()
    {
        var retriever = new WebpageRetriever(new FakeHttpHandler());
        var actual = await Assert.ThrowsAsync<Exception>(() => retriever.GetTitleAsync("https://www.test-missing-title.com", System.Threading.CancellationToken.None));
        Assert.Equal("Missing webpage's title", actual.Message);
    }
}