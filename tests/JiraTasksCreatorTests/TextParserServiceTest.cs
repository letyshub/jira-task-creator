using System;
using Xunit;

public class TextParserServiceTest
{
    private readonly TextParserService _service = new TextParserService();

    [Theory]
    [InlineData("<title>Test</title>", "Test")]
    [InlineData("aaaaa<title>Test</title>bbbb", "Test")]
    [InlineData("aaaaa<title class=\"test-class\">Test</title>bbbb", "Test")]
    public void GetHtmlTitle_GetsCorrectText_ReturnExpected(string text, string expected)
    {
        var actual = _service.GetHtmlTitle(text);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("<titleTest</title>")]
    [InlineData("<title>Test")]
    [InlineData("<title>Test</title")]
    [InlineData("<title>Testtitle>")]
    [InlineData("Test</title>")]
    [InlineData("<titleeTest</title>")]
    public void GetHtmlTitle_GetsIncorrectText_ThrowsException(string text)
    {
        var actual = Assert.Throws<Exception>(() => _service.GetHtmlTitle(text));
        Assert.Equal("Missing webpage's title", actual.Message);
    }
}