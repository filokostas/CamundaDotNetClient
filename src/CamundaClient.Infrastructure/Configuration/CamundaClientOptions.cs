namespace CamundaClient.Infrastructure.Configuration;
public class CamundaClientOptions
{
	/// <summary>
	/// The base URL for the Camunda REST API.
	/// </summary>
	public string BaseUrl { get; set; } = string.Empty;

	/// <summary>
	/// The authentication token for the API.
	/// </summary>
	public string? AuthenticationToken { get; set; }
}
