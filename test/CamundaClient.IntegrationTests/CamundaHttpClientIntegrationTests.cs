using CamundaClient.Core.Services.Interfaces;
using CamundaClient.Infrastructure;
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
	public async Task StartProcessAsync_ValidRequest_ReturnsProcessInstanceWithVariables()
	{
		// Arrange
		var variables = new Dictionary<string, object>
		{
			{ "amount", 1000 },
			{ "approved", true }
		};

		// Act
		var result = await _client.StartProcessAsync("ReviewInvoice", "businessKey123", variables, true);

		// Assert
		Assert.NotNull(result);
		Assert.NotNull(result.Id);
		Assert.Equal("businessKey123", result.BusinessKey);
		Assert.NotNull(result.Variables);
		Assert.True(result.Variables.ContainsKey("amount"));
		Assert.Equal(1000L, result.Variables["amount"].Value);
	}
}