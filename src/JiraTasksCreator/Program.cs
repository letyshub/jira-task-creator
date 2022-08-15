using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();
var settings = config.GetRequiredSection("Settings").Get<ApplicationSettings>();

var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHttpClient("Jira", c =>
                   {
                       c.BaseAddress = new Uri(settings.Jira.Url);
                       c.DefaultRequestHeaders.Authorization =
                           new AuthenticationHeaderValue("Basic", HttpUtiltyService.CreateBasicAuthValue(settings.Jira.User, settings.Jira.Token));
                   });
                   services.AddScoped<IJiraTaskCreator, JiraTaskCreator>();
                   services.AddScoped<IHttpHandler, HttpHandler>();
                   services.AddScoped<IWebpageRetriever, WebpageRetriever>();
                   services.AddSingleton<ApplicationSettings>(settings);
                   services.AddSingleton<JiraConfiguration>(settings.Jira);
                   services.AddSingleton<JiraTaskCreatorApplication>();
               }).UseConsoleLifetime();

var host = builder.Build();

using (var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    try
    {
        var cts = new CancellationTokenSource(settings.Timeout * 1000 * 60);
        var myService = services.GetRequiredService<JiraTaskCreatorApplication>();
        await myService.RunAsync(cts.Token);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error Occured");
    }
}

return 0;
