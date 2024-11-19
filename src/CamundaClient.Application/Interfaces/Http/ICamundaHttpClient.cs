using CamundaClient.Application.Dtos.Responses;

namespace CamundaClient.Application.Interfaces.Http;
public interface ICamundaHttpClient
{
	Task<T> GetAsync<T>(string endpoint);
	Task<T> PostAsync<T>(string endpoint, object content);
	Task DeleteAsync(string endpoint);
}