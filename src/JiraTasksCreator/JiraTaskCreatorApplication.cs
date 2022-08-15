public class JiraTaskCreatorApplication
{
    private readonly IJiraTaskCreator _jiraTaskCreator;

    public JiraTaskCreatorApplication(IJiraTaskCreator jiraTaskCreator)
    {
        _jiraTaskCreator = jiraTaskCreator;
    }

    public Task<string> RunAsync(CancellationToken ct)
    {
        return Task.FromResult<string>("run");
    }
}