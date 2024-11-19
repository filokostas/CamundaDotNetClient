using CamundaClient.Application.Dtos.Exceptions;
using CamundaClient.Infrastructure.Exceptions;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace CamundaClient.Infrastructure.Handlers;
public class HttpResponseHandler : IHttpResponseHandler
{
    private readonly ILogger<HttpResponseHandler> _logger;
    private readonly IJsonSerializer _jsonSerializer;

    public HttpResponseHandler(ILogger<HttpResponseHandler> logger, IJsonSerializer jsonSerializer)
    {
        _logger = logger;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<T> HandleResponse<T>(HttpResponseMessage response, string endpoint)
    {
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to {Method} request to {Endpoint} with status code {StatusCode}",
                response.RequestMessage?.Method, endpoint, response.StatusCode);
            await HandleErrorResponse(response, endpoint);
        }

        var content = await response.Content.ReadAsStringAsync();
        _logger.LogDebug("Response content: {Content}", content);

        return _jsonSerializer.Deserialize<T>(content)
               ?? throw new InvalidOperationException($"Response content is null for {endpoint}");
    }

    private async Task HandleErrorResponse(HttpResponseMessage response, string endpoint)
    {
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogError("Error response content from {Endpoint}: {Content}", endpoint, content);

        // Deserialize base exception
        var baseException = _jsonSerializer.Deserialize<CamundaException>(content);

        if (baseException != null)
        {
            // Decode specialized exception
            var specializedException = DecodeCamundaException(content, baseException.Type);

            _logger.LogError(
                "Handled error for {Endpoint} with StatusCode {StatusCode}: {ErrorDetails}",
                endpoint, response.StatusCode, specializedException);

            throw new CamundaApiException(
                $"Error for {endpoint}",
                (int)response.StatusCode,
                specializedException
            );
        }

        // If no specialized exception is found, throw a generic exception
        _logger.LogError("Unhandled error for {Endpoint} with StatusCode {StatusCode}", endpoint, response.StatusCode);
        throw new HttpRequestException($"Request to {endpoint} failed with status code {response.StatusCode}");
    }

    private CamundaException? DecodeCamundaException(string content, string? type) => type switch
    {
        "AuthorizationExceptionDto" => _jsonSerializer.Deserialize<AuthorizationException>(content),
        "ParseExceptionDto" => _jsonSerializer.Deserialize<ParseException>(content),
        _ => _jsonSerializer.Deserialize<CamundaException>(content)
    };
}
