
namespace CamundaClient.Infrastructure.Test;
public class MockHttpMessageHandler : HttpMessageHandler
{
	private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _handlerFunc;

	public MockHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> handlerFunc)
	{
		_handlerFunc = handlerFunc ?? throw new ArgumentNullException(nameof(handlerFunc));
	}
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		return _handlerFunc(request);
	}
}
