using System.Text.Json.Serialization;

public class JiraCreateTaskRequest
{
    [JsonPropertyName("fields")]
    public JiraFields? Fields { get; set; }

    public JiraCreateTaskRequest(string description, string summary, string project, string? parentId)
    {
        this.Fields = new JiraFields
        {
            Description = description,
            Summary = summary,
            Project = new JiraProject { Key = project },
            Parent = string.IsNullOrEmpty(parentId)
                ? null
                : new JiraParent { Id = parentId }
        };
    }
}

public class JiraFields
{
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("project")]
    public JiraProject? Project { get; set; }

    [JsonPropertyName("parent")]
    public JiraParent? Parent { get; set; }

    [JsonPropertyName("issuetype")]
    public JiraIssueType IssueType => new JiraIssueType();
}

public class JiraProject
{
    [JsonPropertyName("key")]
    public string? Key { get; set; }
}

public class JiraParent
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

public class JiraIssueType
{
    [JsonPropertyName("name")]
    public string Name => "Task";
}