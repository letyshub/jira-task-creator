public class JiraConfiguration
{
    public string Project { get; set; } = string.Empty;
    public string? TaskParentId { get; set; }
    public string User { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}