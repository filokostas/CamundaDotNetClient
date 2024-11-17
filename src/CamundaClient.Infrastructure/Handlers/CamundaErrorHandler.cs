using CamundaClient.Application.Dtos.Responses;
using CamundaClient.Infrastructure.Exceptions;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace CamundaClient.Infrastructure.Handlers;
public class CamundaErrorHandler : ICamundaErrorHandler
{
	private readonly IJsonSerializer _jsonSerializer;
	private readonly ILogger<CamundaErrorHandler> _logger;

	public CamundaErrorHandler(IJsonSerializer jsonSerializer, ILogger<CamundaErrorHandler> logger)
	{
		_jsonSerializer = jsonSerializer;
		_logger = logger;
	}

	public async Task HandleErrorResponseAsync(HttpResponseMessage response, string endpoint)
	{
		if (response.IsSuccessStatusCode) return;

		var errorContent = await response.Content.ReadAsStringAsync();
		CamundaError? errorDetails = null;

		try
		{
			errorDetails = _jsonSerializer.Deserialize<CamundaError>(errorContent);
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Failed to deserialize error details for {Endpoint}", endpoint);
		}

		_logger.LogError("Request to {Endpoint} failed. StatusCode: {StatusCode}, Error: {ErrorContent}",
			endpoint, response.StatusCode, errorContent);

		throw new CamundaApiException($"Camunda API error for {endpoint}. Status code: {response.StatusCode}",
			(int)response.StatusCode, errorDetails);
	}
}
