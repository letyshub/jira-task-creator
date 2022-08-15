public class ApplicationSettings
{
    public JiraConfiguration Jira { get; set; } = null!;
    public int Timeout { get; set; } = 10;
    public string LinksFilePath { get; set; } = string.Empty;
}