using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace CamundaClient.Infrastructure.Http;

public class CamundaHttpClient : ICamundaHttpClient
{
	private readonly HttpClient _httpClient;
	private readonly IHttpRequestHandler _httpRequestHandler;
	private readonly IHttpResponseHandler _httpResponseHandler;
	private readonly ILogger<CamundaHttpClient> _logger;

	public CamundaHttpClient(
		HttpClient httpClient,
		IHttpRequestHandler httpRequestHandler,
		IHttpResponseHandler httpResponseHandler,
		ILogger<CamundaHttpClient> logger)
	{
		_httpClient = httpClient;
		_httpRequestHandler = httpRequestHandler;
		_httpResponseHandler = httpResponseHandler;
		_logger = logger;
	}

	public async Task<T> GetAsync<T>(string endpoint)
	{
		var request = _httpRequestHandler.CreateRequest(HttpMethod.Get, endpoint);
		var response = await SendRequestAsync(request, endpoint);
		return await _httpResponseHandler.HandleResponse<T>(response, endpoint);
	}

	public async Task<T> PostAsync<T>(string endpoint, object content)
	{
		var request = _httpRequestHandler.CreateRequest(HttpMethod.Post, endpoint, content);
		var response = await SendRequestAsync(request, endpoint);
		return await _httpResponseHandler.HandleResponse<T>(response, endpoint);
	}

	public async Task DeleteAsync(string endpoint)
	{
		var request = _httpRequestHandler.CreateRequest(HttpMethod.Delete, endpoint);
		var response = await SendRequestAsync(request, endpoint);
		await _httpResponseHandler.HandleResponse<object>(response, endpoint);
	}

	private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, string endpoint)
	{
		_logger.LogInformation("Sending {Method} request to {Endpoint}", request.Method, endpoint);
		var response = await _httpClient.SendAsync(request);
		_logger.LogDebug("Received response for {Endpoint} with status code {StatusCode}", endpoint, response.StatusCode);
		return response;
	}
}

