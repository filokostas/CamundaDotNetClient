using CamundaClient.Infrastructure.Configuration;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace CamundaClient.Infrastructure.Test;
public class CamundaClientTests
{
    private readonly IServiceCollection _services;
    private readonly Mock<IOptions<CamundaClientOptions>> _mockOptions;
    private readonly Mock<ILogger<IHttpRequestHandler>> _mockLogger;
    private readonly Mock<IJsonSerializer> _mockJsonSerializer;
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;

    public CamundaClientTests()
    {
        _services = new ServiceCollection();
        _mockOptions = new Mock<IOptions<CamundaClientOptions>>();
        _mockLogger = new Mock<ILogger<IHttpRequestHandler>>();
        _mockJsonSerializer = new Mock<IJsonSerializer>();
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
    }

    [Fact]
    public void AddCamundaHttpClient_ConfiguresHttpClient()
    {
        // Arrange
        var options = new CamundaClientOptions { BaseUrl = "https://example.com" };
        _mockOptions.Setup(o => o.Value).Returns(options);
        _services.AddSingleton(_mockOptions.Object);
        _services.AddSingleton(_mockLogger.Object);

        // Act
        _services.AddCamundaHttpClient(o => o.BaseUrl = "https://example.com");
        var provider = _services.BuildServiceProvider();
        var clientFactory = provider.GetRequiredService<IHttpClientFactory>();
        var client = clientFactory.CreateClient(nameof(IHttpRequestHandler));

        // Assert
        Assert.Equal(new Uri("https://example.com"), client.BaseAddress);
    }
}
