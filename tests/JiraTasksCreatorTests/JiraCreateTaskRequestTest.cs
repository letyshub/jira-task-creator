using Xunit;

public class JiraCreateTaskRequestTest
{
    [Theory]
    [InlineData("description", "summary", "project", "parent")]
    [InlineData("description", "summary", "project", "")]
    [InlineData("description", "summary", "project", null)]
    public void Should_Create_Correct_Request(string description, string summary, string project, string? parentId)
    {
        var actual = new JiraCreateTaskRequest(description, summary, project, parentId);
        Assert.Equal(description, actual.Fields?.Description);
        Assert.Equal(summary, actual.Fields?.Summary);
        Assert.Equal(project, actual.Fields?.Project?.Key);

        if (string.IsNullOrEmpty(parentId))
        {
            Assert.Null(actual.Fields?.Parent);
        }
        else
        {
            Assert.Equal(parentId, actual.Fields?.Parent?.Id);
        }
    }
}