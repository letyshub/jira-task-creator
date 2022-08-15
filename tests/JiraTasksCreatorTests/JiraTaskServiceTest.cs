using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

public class JiraTaskServiceTest
{
    private readonly JiraTaskService _service;

    public JiraTaskServiceTest()
    {
        var clientHandlerStub = new FakeHttpHandler();
        var client = new HttpClient(clientHandlerStub);
        var mockHttpClientFactory = new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

        var mockWebpageRetriever = new Mock<IWebpageRetriever>();

        _service = new JiraTaskService(CreateTestConfig(), mockHttpClientFactory.Object, mockWebpageRetriever.Object);
    }

    [Fact]
    public async Task Should_Send_Create_Task_Request()
    {
        await _service.CreateTaskAsync("http://create-task.com", CancellationToken.None);
    }

    private static JiraConfiguration CreateTestConfig()
    {
        return new JiraConfiguration
        {
            Project = "test-project",
            Token = "1212432543",
            Url = "https://www.test.com/",
            User = "test-user"
        };
    }
}