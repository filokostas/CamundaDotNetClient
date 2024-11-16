using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;
using CamundaClient.Application.Interfaces;
using CamundaClient.Core.Services;
using CamundaClient.Infrastructure.Configuration;
using CamundaClient.Infrastructure.Interfaces;
using CamundaClient.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Registry;
using Polly.Retry;
using System.Net;
using System.Text;

namespace CamundaClient.Infrastructure.Test.Http;
public class CamundaClientTests
{
	private readonly IServiceCollection _services;
	private readonly Mock<IOptions<CamundaClientOptions>> _mockOptions;
	private readonly Mock<ILogger<ICamundaHttpClient>> _mockLogger;
	private readonly Mock<IJsonSerializer> _mockJsonSerializer;
	private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;

	public CamundaClientTests()
	{
		_services = new ServiceCollection();
		_mockOptions = new Mock<IOptions<CamundaClientOptions>>();
		_mockLogger = new Mock<ILogger<ICamundaHttpClient>>();
		_mockJsonSerializer = new Mock<IJsonSerializer>();
		_mockHttpMessageHandler = new Mock<HttpMessageHandler>();
	}

	[Fact]
	public async Task StartProcessAsync_ReturnsProcessInstanceWithVariables()
	{
		var expectedResponse = new ProcessInstanceWithVariables
		{
			Id = "12345",
			DefinitionId = "processDef123",
			BusinessKey = "businessKey123",
			Variables = new Dictionary<string, CamundaVariable>
			{
				{ "amount", CamundaVariable.Create(1000, "Integer") },
				{ "approved", CamundaVariable.Create(true, "Boolean") }
			}
		};

		// Mock Serialize and Deserialize
		_mockJsonSerializer
			.Setup(s => s.Serialize(It.IsAny<StartProcessInstance>()))
			.Returns("{}"); // Simulated serialized request

		_mockJsonSerializer
			.Setup(s => s.Deserialize<ProcessInstanceWithVariables>(It.IsAny<string>()))
			.Returns(expectedResponse);

		var responseMessage = new HttpResponseMessage
		{
			StatusCode = HttpStatusCode.OK,
			Content = new StringContent("{}")
		};

		_mockHttpMessageHandler
			.Protected().Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(responseMessage);

		var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
		{
			BaseAddress = new Uri("http://localhost:8080/engine-rest")
		};

		var camundaClient = new CamundaHttpClient(httpClient, _mockLogger.As<ILogger<CamundaHttpClient>>().Object, _mockJsonSerializer.Object);

		var variables = new Dictionary<string, object>
		{
			{ "amount", 1000 },
			{ "approved", true }
		};

		// Act
		var result = await camundaClient.StartProcessAsync("processKey123", "businessKey123", variables);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("12345", result.Id);
		Assert.Equal("processDef123", result.DefinitionId);
		Assert.Equal("businessKey123", result.BusinessKey);
		Assert.NotNull(result.Variables);
		Assert.Equal(2, result.Variables.Count);
		Assert.Equal(1000, result.Variables["amount"].Value);
		Assert.Equal("Integer", result.Variables["amount"].Type);
		Assert.Equal(true, result.Variables["approved"].Value);
		Assert.Equal("Boolean", result.Variables["approved"].Type);
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
		var client = clientFactory.CreateClient(nameof(ICamundaHttpClient));

		// Assert
		Assert.Equal(new Uri("https://example.com"), client.BaseAddress);
	}
}
