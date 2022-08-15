public class JiraCreateTaskRequest
{
    public JiraFields? Fields { get; set; }
}

public class JiraFields
{
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public JiraProject? Project { get; set; }
    public JiraParent? Parent { get; set; }
    public JiraIssueType IssueType => new JiraIssueType();
}

public class JiraProject
{
    public string? Key { get; set; }
}

public class JiraParent
{
    public string? Id { get; set; }
}

public class JiraIssueType
{
    public string Name => "Task";
}