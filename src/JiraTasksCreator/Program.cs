using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(".//logs//log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .CreateLogger();

try
{
    Log.Logger.Information("Starting application");
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
                       services.AddHttpClient("Default", c =>
                       {
                           c.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html; charset=utf-8");
                           c.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36");
                       });
                       services.AddScoped<IJiraTaskService, JiraTaskService>();
                       services.AddScoped<IWebpageRetriever, WebpageRetriever>();
                       services.AddScoped<ITextParserService, TextParserService>();
                       services.AddSingleton<ApplicationSettings>(settings);
                       services.AddSingleton<JiraConfiguration>(settings.Jira);
                       services.AddSingleton<JiraTaskCreatorApplication>();
                   })
                   .UseSerilog()
                   .UseConsoleLifetime();

    var host = builder.Build();

    using (var serviceScope = host.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        var cts = new CancellationTokenSource(settings.Timeout * 1000 * 60);
        var myService = services.GetRequiredService<JiraTaskCreatorApplication>();
        await myService.RunAsync(cts.Token);
    }

    Log.Logger.Information("Closing application");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

return 0;
