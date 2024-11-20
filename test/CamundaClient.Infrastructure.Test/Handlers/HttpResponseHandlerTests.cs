using CamundaClient.Application.Dtos.Exceptions;
using CamundaClient.Infrastructure.Exceptions;
using CamundaClient.Infrastructure.Handlers;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace CamundaClient.Infrastructure.Test.Handlers;

public class HttpResponseHandlerTests
{
	private readonly Mock<ILogger<HttpResponseHandler>> _loggerMock;
	private readonly Mock<IJsonSerializer> _jsonSerializerMock;
	private readonly HttpResponseHandler _httpResponseHandler;

	public HttpResponseHandlerTests()
	{
		_loggerMock = new Mock<ILogger<HttpResponseHandler>>();
		_jsonSerializerMock = new Mock<IJsonSerializer>();
		_httpResponseHandler = new HttpResponseHandler(_loggerMock.Object, _jsonSerializerMock.Object);
	}

	[Fact]
	public async Task HandleResponse_ShouldReturnDeserializedObject_OnSuccess()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent("{\"key\":\"value\"}")
		};
		_jsonSerializerMock.Setup(js => js.Deserialize<Dictionary<string, string>>(It.IsAny<string>()))
			.Returns(new Dictionary<string, string> { { "key", "value" } });

		// Act
		var result = await _httpResponseHandler.HandleResponse<Dictionary<string, string>>(responseMessage, endpoint);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("value", result["key"]);
	}

	[Fact]
	public async Task HandleResponse_ShouldThrowCamundaApiException_OnError()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
		{
			Content = new StringContent("{\"type\":\"CamundaException\"}")
		};
		_jsonSerializerMock.Setup(js => js.Deserialize<CamundaException>(It.IsAny<string>()))
			.Returns(new CamundaException { Type = "CamundaException" });

		// Act & Assert
		await Assert.ThrowsAsync<CamundaApiException>(() => _httpResponseHandler.HandleResponse<object>(responseMessage, endpoint));
	}

	[Fact]
	public async Task HandleResponse_ShouldThrowHttpRequestException_OnUnhandledError()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
		{
			Content = new StringContent("{\"type\":\"UnknownException\"}")
		};
		_jsonSerializerMock.Setup(js => js.Deserialize<CamundaException>(It.IsAny<string>()))
			.Returns((CamundaException)null);

		// Act & Assert
		await Assert.ThrowsAsync<HttpRequestException>(() => _httpResponseHandler.HandleResponse<object>(responseMessage, endpoint));
	}

	[Fact]
	public async Task HandleResponse_ShouldDeserializeSpecializedException_WhenTypeIsAuthorizationException()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.Forbidden)
		{
			Content = new StringContent("{\"type\":\"AuthorizationExceptionDto\"}")
		};
		_jsonSerializerMock.Setup(js => js.Deserialize<CamundaException>(It.IsAny<string>()))
			.Returns(new CamundaException { Type = "AuthorizationExceptionDto" });
		_jsonSerializerMock.Setup(js => js.Deserialize<AuthorizationException>(It.IsAny<string>()))
			.Returns(new AuthorizationException());

		// Act & Assert
		await Assert.ThrowsAsync<CamundaApiException>(() => _httpResponseHandler.HandleResponse<object>(responseMessage, endpoint));
	}

	[Fact]
	public async Task HandleResponse_ShouldDeserializeSpecializedException_WhenTypeIsParseException()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
		{
			Content = new StringContent("{\"type\":\"ParseExceptionDto\"}")
		};
		_jsonSerializerMock.Setup(js => js.Deserialize<CamundaException>(It.IsAny<string>()))
			.Returns(new CamundaException { Type = "ParseExceptionDto" });
		_jsonSerializerMock.Setup(js => js.Deserialize<ParseException>(It.IsAny<string>()))
			.Returns(new ParseException());

		// Act & Assert
		await Assert.ThrowsAsync<CamundaApiException>(() => _httpResponseHandler.HandleResponse<object>(responseMessage, endpoint));
	}

	[Fact]
	public async Task HandleResponse_ShouldNotThrowException_WhenResponseIsNoContent()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
		{
			Content = new StringContent(string.Empty)
		};

		// Act
		var result = await _httpResponseHandler.HandleResponse<object>(responseMessage, endpoint);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public async Task HandleResponse_ShouldThrowInvalidOperationException_WhenResponseContentIsNull()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent(string.Empty)
		};
		_jsonSerializerMock.Setup(js => js.Deserialize<object>(It.IsAny<string>()))
			.Returns((object)null);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _httpResponseHandler.HandleResponse<object>(responseMessage, endpoint));
		Assert.Equal($"Response content is null for {endpoint}", exception.Message);
	}

	[Fact]
	public async Task HandleResponse_ShouldThrowException_WhenResponseContentIsMalformedJson()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent("malformed json")
		};
		_jsonSerializerMock.Setup(js => js.Deserialize<object>(It.IsAny<string>()))
			.Throws(new DeserializationException("Failed to deserialize JSON: malformed json"));

		// Act & Assert
		await Assert.ThrowsAsync<DeserializationException>(() => _httpResponseHandler.HandleResponse<object>(responseMessage, endpoint));
	}

	[Fact]
	public async Task HandleResponse_ShouldReturnDefaultValue_WhenResponseContentIsEmpty()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent(string.Empty)
		};
		_jsonSerializerMock.Setup(js => js.Deserialize<object>(It.IsAny<string>()))
			.Returns((object)null);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _httpResponseHandler.HandleResponse<object>(responseMessage, endpoint));
		Assert.Equal($"Response content is null for {endpoint}", exception.Message);
	}

	[Fact]
	public async Task HandleResponse_ShouldHandleTimeoutException()
	{
		// Arrange
		var endpoint = "http://localhost:8080/engine-rest/";
		var cancellationTokenSource = new CancellationTokenSource();
		cancellationTokenSource.Cancel();
		var responseTask = Task.FromCanceled<HttpResponseMessage>(cancellationTokenSource.Token);

		// Act & Assert
		await Assert.ThrowsAsync<TaskCanceledException>(async () => _httpResponseHandler.HandleResponse<object>(await responseTask, endpoint));
	}
}
