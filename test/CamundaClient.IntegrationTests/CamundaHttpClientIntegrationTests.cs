using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Infrastructure;
using CamundaClient.Infrastructure.Interfaces;
using CamundaClient.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CamundaClient.IntegrationTests;

public class CamundaHttpClientIntegrationTests
{
    private readonly ICamundaHttpClient _client;

    public CamundaHttpClientIntegrationTests()
    {
        var services = new ServiceCollection();

        services.AddCamundaHttpClient(options =>
        {
            options.BaseUrl = "http://localhost:8080/engine-rest/";
            // Uncomment and add token if authentication is required
            // options.AuthenticationToken = "your-auth-token";
        });

        var serviceProvider = services.BuildServiceProvider();
        _client = serviceProvider.GetRequiredService<ICamundaHttpClient>();
    }

    [Fact]
    public void DependencyInjection_ShouldRegisterIJsonSerializer()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddCamundaHttpClient(options => options.BaseUrl = "http://example.com");

        var provider = services.BuildServiceProvider();

        // Act
        var serializer = provider.GetService<IJsonSerializer>();

        // Assert
        Assert.NotNull(serializer);
        Assert.IsType<NewtonsoftJsonSerializer>(serializer);
    }
}