namespace CamundaClient.Infrastructure.Interfaces;

public interface IHttpRequestHandler
{
    Task<HttpResponseMessage> SendAsync(HttpMethod method, string endpoint, object? content = null);
}
