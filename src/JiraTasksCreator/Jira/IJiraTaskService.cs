public interface IJiraTaskService
{
    Task CreateTaskAsync(string url, CancellationToken ct);
}