using System.Net.Http.Headers;

public static class HttpUtiltyService
{
    public static string CreateBasicAuthValue(string username, string password)
    {
        return Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes($"{username}:{password}"));
    }
}