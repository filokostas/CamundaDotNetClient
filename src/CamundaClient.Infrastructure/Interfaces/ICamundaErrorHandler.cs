namespace CamundaClient.Infrastructure.Interfaces;
public interface ICamundaErrorHandler
{
	Task HandleErrorResponseAsync(HttpResponseMessage response, string endpoint);
}
