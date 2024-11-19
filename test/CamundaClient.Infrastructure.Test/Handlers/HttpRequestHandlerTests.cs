using CamundaClient.Infrastructure.Handlers;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;

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
        _httpRequestHandler = new HttpRequestHandler(_httpClient, _loggerMock.Object, _jsonSerializerMock.Object);
    }

    [Fact]
    public async Task SendAsync_ShouldSendGetRequest()
    {
        // Arrange
        var endpoint = "http://localhost:8080/engine-rest/";
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri!.ToString() == endpoint),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act
        var response = await _httpRequestHandler.SendAsync(HttpMethod.Get, endpoint);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri!.ToString() == endpoint),
            ItExpr.IsAny<CancellationToken>());
    }

    //[Fact]
    //public async Task SendAsync_ShouldSendPostRequestWithContent()
    //{
    //    // Arrange
    //    var endpoint = "https://example.com";
    //    var content = new { Name = "Test" };
    //    var jsonContent = "{\"Name\":\"Test\"}";
    //    var responseMessage = new HttpResponseMessage(HttpStatusCode.Created);
    //    _jsonSerializerMock.Setup(s => s.Serialize(content)).Returns(jsonContent);
    //    _httpMessageHandlerMock
    //        .Protected()
    //        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString() == endpoint && req.Content.ReadAsStringAsync().Result == jsonContent), ItExpr.IsAny<CancellationToken>())
    //        .ReturnsAsync(responseMessage);

    //    // Act
    //    var response = await _httpRequestHandler.SendAsync(HttpMethod.Post, endpoint, content);

    //    // Assert
    //    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    //    _jsonSerializerMock.VerifyAll();
    //    _httpMessageHandlerMock.VerifyAll();
    //}

    [Fact]
    public async Task SendAsync_ShouldLogInformation()
    {
        // Arrange
        var endpoint = "https://example.com";
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act
        await _httpRequestHandler.SendAsync(HttpMethod.Get, endpoint);

        // Assert
        _loggerMock.Verify(l => l.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Preparing GET request to https://example.com")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);

        _loggerMock.Verify(l => l.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Sending GET request to https://example.com")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }
}
