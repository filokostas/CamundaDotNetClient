using CamundaClient.Core.Exceptions;
using CamundaClient.Core.Models.Requests;
using CamundaClient.Core.Models.Responses;
using CamundaClient.Core.Services;
using CamundaClient.Core.Utilities;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;

namespace CamundaClient.Core.Test.Services;
public class CamundaClientTests
{
	[Fact]
	public async Task StartProcessAsync_ReturnsProcessInstanceWithVariables()
	{
		// Arrange
		var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
		var mockLogger = new Mock<ILogger<CamundaHttpClient>>();
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

		var responseMessage = new HttpResponseMessage
		{
			StatusCode = HttpStatusCode.OK,
			Content = new StringContent(JsonHelper.Serialize(expectedResponse))
		};

		mockHttpMessageHandler
			.Protected().Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(responseMessage);

		var httpClient = new HttpClient(mockHttpMessageHandler.Object)
		{
			BaseAddress = new Uri("http://localhost:8080/engine-rest")
		};

		var camundaClient = new CamundaHttpClient(httpClient, mockLogger.Object);

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
		Assert.Equal(1000L, result.Variables["amount"].Value);
		Assert.Equal("Integer", result.Variables["amount"].Type);
		Assert.Equal(true, result.Variables["approved"].Value);
		Assert.Equal("Boolean", result.Variables["approved"].Type);
	}
}
