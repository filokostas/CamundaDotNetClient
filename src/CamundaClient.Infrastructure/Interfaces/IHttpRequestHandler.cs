namespace CamundaClient.Infrastructure.Interfaces;

public interface IHttpRequestHandler
{
	HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, object? content = null);
}
