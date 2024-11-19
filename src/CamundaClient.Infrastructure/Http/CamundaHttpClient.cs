using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Infrastructure.Interfaces;

namespace CamundaClient.Infrastructure.Http;

public class CamundaHttpClient : ICamundaHttpClient
{
    private readonly IHttpRequestHandler _httpRequestHandler;
    private readonly IHttpResponseHandler _httpResponseHandler;

    public CamundaHttpClient(
        IHttpRequestHandler httpRequestHandler,
        IHttpResponseHandler httpResponseHandler)
    {
        _httpRequestHandler = httpRequestHandler;
        _httpResponseHandler = httpResponseHandler;
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpRequestHandler.SendAsync(HttpMethod.Get, endpoint);
        return await _httpResponseHandler.HandleResponse<T>(response, endpoint);
    }

    public async Task<T> PostAsync<T>(string endpoint, object content)
    {
        var response = await _httpRequestHandler.SendAsync(HttpMethod.Post, endpoint, content);
        return await _httpResponseHandler.HandleResponse<T>(response, endpoint);
    }

    public async Task DeleteAsync(string endpoint)
    {
        var response = await _httpRequestHandler.SendAsync(HttpMethod.Delete, endpoint);
        await _httpResponseHandler.HandleResponse<object>(response, endpoint);
    }
}

