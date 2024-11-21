namespace CamundaClient.Infrastructure.Configuration;

/// <summary>
/// Represents the resilience options for configuring retry, timeout, and circuit breaker policies.
/// </summary>
public class ResilienceOptions
{
	/// <summary>
	/// Gets or sets the number of retry attempts.
	/// </summary>
	/// <value>The default value is 3.</value>
	public int RetryCount { get; set; } = 3;

	/// <summary>
	/// Gets or sets the timeout duration in seconds.
	/// </summary>
	/// <value>The default value is 10 seconds.</value>
	public int TimeoutSeconds { get; set; } = 10;

	/// <summary>
	/// Gets or sets the number of failures before the circuit breaker is triggered.
	/// </summary>
	/// <value>The default value is 2.</value>
	public int CircuitBreakerFailures { get; set; } = 2;

	/// <summary>
	/// Gets or sets the duration in seconds for which the circuit breaker remains open.
	/// </summary>
	/// <value>The default value is 30 seconds.</value>
	public int CircuitBreakerDurationSeconds { get; set; } = 30;
}
