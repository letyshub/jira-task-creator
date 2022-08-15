public interface IJiraTaskCreator
{
    Task CreateTaskAsync(string url, CancellationToken ct);
}