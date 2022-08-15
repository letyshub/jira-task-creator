using Xunit;

public class HttpUtiltyServiceTest
{
    [Theory]
    [InlineData("user", "password", "dXNlcjpwYXNzd29yZA==")]
    [InlineData("user", "", "dXNlcjo=")]
    [InlineData("", "password", "OnBhc3N3b3Jk")]
    public void Should_Create_Correct_Value(string username, string password, string expected)
    {
        var actual = HttpUtiltyService.CreateBasicAuthValue(username, password);
        Assert.Equal(expected, actual);
    }
}