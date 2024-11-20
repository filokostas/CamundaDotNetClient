using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;

namespace CamundaClient.Infrastructure.Handlers;

public class HttpRequestHandler : IHttpRequestHandler
{
	private readonly ILogger<HttpRequestHandler> _logger;
	private readonly IJsonSerializer _jsonSerializer;

	public HttpRequestHandler(ILogger<HttpRequestHandler> logger, IJsonSerializer jsonSerializer)
	{
		_logger = logger;
		_jsonSerializer = jsonSerializer;
	}

	public HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, object? content = null)
	{
		_logger.LogInformation("Preparing {Method} request to {Endpoint}", method, endpoint);
		var request = new HttpRequestMessage(method, endpoint);
		if (content != null)
		{
			var json = _jsonSerializer.Serialize(content);
			_logger.LogDebug("Serializing content to JSON: {Json}", json);
			request.Content = new StringContent(json, Encoding.UTF8, "application/json");
		}

		//// Set default headers
		//request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		//request.Headers.UserAgent.ParseAdd("HttpRequestHandler");

		return request;
	}
}
