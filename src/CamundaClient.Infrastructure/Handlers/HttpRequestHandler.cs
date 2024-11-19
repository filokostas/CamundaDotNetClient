using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;

namespace CamundaClient.Infrastructure.Handlers;

public class HttpRequestHandler : IHttpRequestHandler
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpRequestHandler> _logger;
    private readonly IJsonSerializer _jsonSerializer;

    public HttpRequestHandler(HttpClient httpClient, ILogger<HttpRequestHandler> logger, IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _logger = logger;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string endpoint, object? content = null)
    {
        _logger.LogInformation("Preparing {Method} request to {Endpoint}", method, endpoint);
        var request = new HttpRequestMessage(method, endpoint);
        if (content != null)
        {
            var json = _jsonSerializer.Serialize(content);
            _logger.LogDebug("Serializing content to JSON: {Json}", json);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        _logger.LogInformation("Sending {Method} request to {Endpoint}", method, endpoint);
        var response = await _httpClient.SendAsync(request);
        _logger.LogDebug("Received response for {Endpoint} with status code {StatusCode}", endpoint, response.StatusCode);

        return response;
    }
}
