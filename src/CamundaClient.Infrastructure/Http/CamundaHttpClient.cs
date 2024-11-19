using CamundaClient.Application.Dtos.Exceptions;
using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Infrastructure.Exceptions;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace CamundaClient.Infrastructure.Http;

public class CamundaHttpClient : ICamundaHttpClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<CamundaHttpClient> _logger;
	private readonly IJsonSerializer _jsonSerializer;

	public CamundaHttpClient(
		HttpClient httpClient,
		ILogger<CamundaHttpClient> logger,
		IJsonSerializer jsonSerializer)
	{
		_httpClient = httpClient;
		_logger = logger;
		_jsonSerializer = jsonSerializer;
	}

	public async Task<T> GetAsync<T>(string endpoint)
	{
		_logger.LogInformation("Preparing GET request to {Endpoint}", endpoint);
		var response = await SendAsync(HttpMethod.Get, endpoint);
		return await EnsureNonNullResponse<T>(response, endpoint);
	}

	public async Task<T> PostAsync<T>(string endpoint, object content)
	{
		var jsonContent = _jsonSerializer.Serialize(content);
		_logger.LogInformation("Preparing POST request to {Endpoint} with content: {Content}", endpoint, jsonContent);
		var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
		var response = await SendAsync(HttpMethod.Post, endpoint, httpContent);
		return await EnsureNonNullResponse<T>(response, endpoint);
	}

	public async Task DeleteAsync(string endpoint)
	{
		_logger.LogInformation("Preparing DELETE request to {Endpoint}", endpoint);
		var response = await SendAsync(HttpMethod.Delete, endpoint);
		await HandleResponse<object>(response, endpoint);
	}

	private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string endpoint, HttpContent? content = null)
	{
		_logger.LogDebug("Sending {Method} request to {Endpoint} with Content: {Content}",
			method, endpoint, content == null ? "No Content" : await content.ReadAsStringAsync());

		var request = new HttpRequestMessage(method, endpoint) { Content = content };
		var response = await _httpClient.SendAsync(request);

		_logger.LogDebug("Received response with StatusCode {StatusCode} for {Endpoint}",
			response.StatusCode, endpoint);

		if (!response.IsSuccessStatusCode)
		{
			_logger.LogWarning("Request to {Endpoint} failed with StatusCode {StatusCode}", endpoint, response.StatusCode);
		}

		return response;
	}

	private async Task<T?> HandleResponse<T>(HttpResponseMessage response, string endpoint)
	{
		if (response.IsSuccessStatusCode)
		{
			var responseContent = await response.Content.ReadAsStringAsync();
			_logger.LogDebug("Successful response from {Endpoint} with Content: {ResponseContent}",
				endpoint, responseContent);

			return _jsonSerializer.Deserialize<T>(responseContent);
		}

		_logger.LogError("Unsuccessful response from {Endpoint}. Handling error...", endpoint);
		await HandleErrorResponse(response, endpoint);
		return default;
	}

	private async Task<T> EnsureNonNullResponse<T>(HttpResponseMessage response, string endpoint)
	{
		var result = await HandleResponse<T>(response, endpoint);

		if (result == null)
		{
			throw new InvalidOperationException(
				$"The API response was null for endpoint '{endpoint}'. Expected a valid object of type {typeof(T).Name}."
			);
		}

		return result;
	}

	private async Task HandleErrorResponse(HttpResponseMessage response, string endpoint)
	{
		var content = await response.Content.ReadAsStringAsync();
		_logger.LogError("Error response content from {Endpoint}: {Content}", endpoint, content);

		var baseException = _jsonSerializer.Deserialize<CamundaException>(content);

		if (baseException != null)
		{
			CamundaException? specializedException = DecodeCamundaException(content, baseException.Type);

			_logger.LogError("Handled error for {Endpoint} with StatusCode {StatusCode}: {ErrorDetails}",
				endpoint, response.StatusCode, specializedException);

			throw new CamundaApiException(
				$"Error for {endpoint}",
				(int)response.StatusCode,
				specializedException
			);
		}

		_logger.LogError("Unhandled error for {Endpoint} with StatusCode {StatusCode}", endpoint, response.StatusCode);
		response.EnsureSuccessStatusCode();
	}

	private static CamundaException? DecodeCamundaException(string content, string? type) => type switch
	{
		"AuthorizationExceptionDto" => JsonConvert.DeserializeObject<AuthorizationException>(content),
		"ParseExceptionDto" => JsonConvert.DeserializeObject<ParseException>(content),
		_ => JsonConvert.DeserializeObject<CamundaException>(content)
	};
}

