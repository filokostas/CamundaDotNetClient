namespace CamundaClient.Infrastructure.Configuration;
public class CamundaClientOptions
{
	/// <summary>
	/// The base URL for the Camunda REST API.
	/// </summary>
	public string BaseUrl { get; set; } = String.Empty;

	/// <summary>
	/// The authentication token for the API.
	/// </summary>
	public string? AuthenticationToken { get; set; }

	/// <summary>
	/// The number of retries to attempt on transient errors.
	/// </summary>
	public int RetryCount { get; set; } = 3;

	/// <summary>
	/// The timeout duration in seconds for API requests.
	/// </summary>
	public int TimeoutSeconds { get; set; } = 10;

	/// <summary>
	/// The number of failures before the circuit breaker trips.
	/// </summary>
	public int CircuitBreakerFailures { get; set; } = 2;

	/// <summary>
	/// The duration in seconds for which the circuit breaker remains open.
	/// </summary>
	public int CircuitBreakerDurationSeconds { get; set; } = 30;
}
