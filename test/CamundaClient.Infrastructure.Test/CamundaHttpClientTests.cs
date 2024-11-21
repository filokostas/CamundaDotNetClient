using CamundaClient.Infrastructure.Http;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;

namespace CamundaClient.Infrastructure.Test;

public class CamundaHttpClientTests
{
	private readonly Mock<IHttpRequestHandler> _mockHttpRequestHandler;
	private readonly Mock<IHttpResponseHandler> _mockHttpResponseHandler;
	private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
	private readonly Mock<ILogger<CamundaHttpClient>> _mockLogger;
	private readonly HttpClient _httpClient;
	private readonly CamundaHttpClient _camundaHttpClient;

	public CamundaHttpClientTests()
	{
		_mockHttpRequestHandler = new Mock<IHttpRequestHandler>();
		_mockHttpResponseHandler = new Mock<IHttpResponseHandler>();
		_mockHttpMessageHandler = new Mock<HttpMessageHandler>();
		_mockLogger = new Mock<ILogger<CamundaHttpClient>>();

		_httpClient = new HttpClient(_mockHttpMessageHandler.Object)
		{
			BaseAddress = new Uri("https://example.com") // Set a base address
		};

		_camundaHttpClient = new CamundaHttpClient(
			_httpClient,
			_mockHttpRequestHandler.Object,
			_mockHttpResponseHandler.Object,
			_mockLogger.Object);
	}

	[Fact]
	public async Task GetAsync_ReturnsExpectedResult()
	{
		// Arrange
		string endpoint = "/test";
		var expectedResult = "result";
		var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
		var response = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent("\"result\"")
		};

		_mockHttpRequestHandler
			.Setup(h => h.CreateRequest(HttpMethod.Get, endpoint, null))
			.Returns(request);

		_mockHttpMessageHandler
			.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(response);

		_mockHttpResponseHandler
			.Setup(h => h.HandleResponse<string>(response, endpoint))
			.ReturnsAsync(expectedResult);

		// Act
		var result = await _camundaHttpClient.GetAsync<string>(endpoint);

		// Assert
		Assert.Equal(expectedResult, result);
		_mockHttpRequestHandler.Verify(h => h.CreateRequest(HttpMethod.Get, endpoint, null), Times.Once);
		_mockHttpResponseHandler.Verify(h => h.HandleResponse<string>(response, endpoint), Times.Once);
	}

	[Fact]
	public async Task PostAsync_ReturnsExpectedResult()
	{
		// Arrange
		string endpoint = "/test";
		var content = new { Data = "test" };
		var expectedResult = "result";
		var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
		var response = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent("\"result\"")
		};

		_mockHttpRequestHandler
			.Setup(h => h.CreateRequest(HttpMethod.Post, endpoint, content))
			.Returns(request);

		_mockHttpMessageHandler
			.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(response);

		_mockHttpResponseHandler
			.Setup(h => h.HandleResponse<string>(response, endpoint))
			.ReturnsAsync(expectedResult);

		// Act
		var result = await _camundaHttpClient.PostAsync<string>(endpoint, content);

		// Assert
		Assert.Equal(expectedResult, result);
		_mockHttpRequestHandler.Verify(h => h.CreateRequest(HttpMethod.Post, endpoint, content), Times.Once);
		_mockHttpResponseHandler.Verify(h => h.HandleResponse<string>(response, endpoint), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CompletesSuccessfully()
	{
		// Arrange
		string endpoint = "/test";
		var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
		var response = new HttpResponseMessage(HttpStatusCode.NoContent);

		_mockHttpRequestHandler
			.Setup(h => h.CreateRequest(HttpMethod.Delete, endpoint, null))
			.Returns(request);

		_mockHttpMessageHandler
			.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(response);

		_mockHttpResponseHandler
			.Setup(h => h.HandleResponse<object>(response, endpoint))
			.Returns(Task.FromResult<object>(null));


		// Act
		await _camundaHttpClient.DeleteAsync(endpoint);

		// Assert
		_mockHttpRequestHandler.Verify(h => h.CreateRequest(HttpMethod.Delete, endpoint, null), Times.Once);
		_mockHttpResponseHandler.Verify(h => h.HandleResponse<object>(response, endpoint), Times.Once);
	}
}
