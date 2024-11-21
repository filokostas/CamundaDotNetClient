using CamundaClient.Infrastructure.Handlers;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;

namespace CamundaClient.Infrastructure.Test.Handlers;

public class HttpRequestHandlerTests
{
	private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
	private readonly HttpClient _httpClient;
	private readonly Mock<ILogger<HttpRequestHandler>> _loggerMock;
	private readonly Mock<IJsonSerializer> _jsonSerializerMock;
	private readonly HttpRequestHandler _httpRequestHandler;

	public HttpRequestHandlerTests()
	{
		_httpMessageHandlerMock = new Mock<HttpMessageHandler>();
		_httpClient = new HttpClient(_httpMessageHandlerMock.Object);
		_loggerMock = new Mock<ILogger<HttpRequestHandler>>();
		_jsonSerializerMock = new Mock<IJsonSerializer>();
		_httpRequestHandler = new HttpRequestHandler(_loggerMock.Object, _jsonSerializerMock.Object);
	}

	[Fact]
	public void CreateRequest_ShouldCreatePostRequestWithContent()
	{
		// Arrange
		var endpoint = "https://example.com";
		var contentObject = new { Name = "Test", Value = 123 };
		var serializedContent = "{ \"Name\": \"Test\", \"Value\": 123 }";

		_jsonSerializerMock
			.Setup(js => js.Serialize(It.IsAny<object>()))
			.Returns(serializedContent);

		// Act
		var actualRequest = _httpRequestHandler.CreateRequest(HttpMethod.Post, endpoint, contentObject);

		// Assert
		Assert.Equal(HttpMethod.Post, actualRequest.Method);
		Assert.Equal(new Uri(endpoint), actualRequest.RequestUri);
		Assert.NotNull(actualRequest.Content);

		var contentString = actualRequest.Content.ReadAsStringAsync().Result;
		Assert.Equal(serializedContent, contentString);
		Assert.Equal("application/json", actualRequest.Content.Headers.ContentType.MediaType);
		Assert.Equal(Encoding.UTF8.WebName, actualRequest.Content.Headers.ContentType.CharSet);
	}

	[Fact]
	public void CreateRequest_ShouldHandleNullContent()
	{
		// Arrange
		var endpoint = "https://example.com";
		object? content = null;

		// Act
		var actualRequest = _httpRequestHandler.CreateRequest(HttpMethod.Post, endpoint, content);

		// Assert
		Assert.Equal(HttpMethod.Post, actualRequest.Method);
		Assert.Equal(new Uri(endpoint), actualRequest.RequestUri);
		Assert.Null(actualRequest.Content);
	}


	[Fact]
	public void CreateRequest_ShouldCreateGetRequest()
	{
		// Arrange
		var endpoint = "https://example.com";
		var expectedRequest = new HttpRequestMessage(HttpMethod.Get, endpoint);

		// Act
		var actualRequest = _httpRequestHandler.CreateRequest(HttpMethod.Get, endpoint);

		// Assert
		Assert.Equal(expectedRequest.Method, actualRequest.Method);
		Assert.Equal(expectedRequest.RequestUri, actualRequest.RequestUri);
		Assert.Null(actualRequest.Content);
	}

	[Fact]
	public void CreateRequest_ShouldLogInformation()
	{
		// Arrange
		var endpoint = "https://example.com";

		// Act
		_httpRequestHandler.CreateRequest(HttpMethod.Get, endpoint);

		// Assert
		_loggerMock.Verify(l => l.Log(
			LogLevel.Information,
			It.IsAny<EventId>(),
			It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Preparing GET request to https://example.com")),
			It.IsAny<Exception>(),
			It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
	}
}
