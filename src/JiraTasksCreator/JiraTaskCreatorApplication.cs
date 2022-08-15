public class JiraTaskCreatorApplication
{
    private readonly IJiraTaskCreator _jiraTaskCreator;
    private readonly ApplicationSettings _settings;

    public JiraTaskCreatorApplication(IJiraTaskCreator jiraTaskCreator, ApplicationSettings settings)
    {
        _jiraTaskCreator = jiraTaskCreator;
        _settings = settings;
    }

    public async Task RunAsync(CancellationToken ct)
    {
        var lines = await System.IO.File.ReadAllLinesAsync(_settings.LinksFilePath, ct);

        foreach (var line in lines)
        {
            await _jiraTaskCreator.CreateTaskAsync(line, ct);
        }

        await System.IO.File.WriteAllLinesAsync(_settings.LinksFilePath, new List<string>(0), ct);
    }
}