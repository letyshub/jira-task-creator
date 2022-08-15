using Microsoft.Extensions.Logging;

public class JiraTaskCreatorApplication
{
    private readonly IJiraTaskService _jiraTaskCreator;
    private readonly ApplicationSettings _settings;
    private readonly ILogger<JiraTaskCreatorApplication> _logger;

    public JiraTaskCreatorApplication(IJiraTaskService jiraTaskCreator, ApplicationSettings settings, ILogger<JiraTaskCreatorApplication> logger)
    {
        _jiraTaskCreator = jiraTaskCreator;
        _settings = settings;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Reading links from {_settings.LinksFilePath}");
        var lines = await System.IO.File.ReadAllLinesAsync(_settings.LinksFilePath, ct);

        foreach (var line in lines)
        {
            _logger.LogInformation($"Creating jira task for {line}");
            await _jiraTaskCreator.CreateTaskAsync(line, ct);
        }

        _logger.LogInformation($"Deleting links from {_settings.LinksFilePath}");
        await System.IO.File.WriteAllLinesAsync(_settings.LinksFilePath, new List<string>(0), ct);
    }
}