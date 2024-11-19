namespace CamundaClient.Infrastructure.Interfaces;

public interface IHttpResponseHandler
{
    Task<T> HandleResponse<T>(HttpResponseMessage response, string endpoint);
}
