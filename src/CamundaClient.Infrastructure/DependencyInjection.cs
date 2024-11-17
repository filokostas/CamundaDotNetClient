﻿using CamundaClient.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Polly.Extensions.Http;
using Polly;
using System.Net;
using CamundaClient.Application.Interfaces;
using CamundaClient.Infrastructure.Interfaces;
using CamundaClient.Infrastructure.Utilities;
using Microsoft.Extensions.Logging;
using CamundaClient.Infrastructure.Http;

namespace CamundaClient.Infrastructure;
public static class DependencyInjection
{
	/// <summary>
	/// Adds the Camunda HTTP client to the service collection with the specified configuration options.
	/// </summary>
	/// <param name="services">The service collection to add the HTTP client to.</param>
	/// <param name="configureOptions">A delegate to configure the <see cref="CamundaClientOptions"/>.</param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddCamundaHttpClient(this IServiceCollection services, Action<CamundaClientOptions> configureOptions)
	{
		services.Configure(configureOptions);

		services.AddTransient<IJsonSerializer, NewtonsoftJsonSerializer>();

		services.AddHttpClient<ICamundaHttpClient, CamundaHttpClient>((provider, client) =>
		{
			var options = provider.GetRequiredService<IOptions<CamundaClientOptions>>().Value;
			var logger = provider.GetRequiredService<ILogger<ICamundaHttpClient>>();

			client.BaseAddress = new Uri(options.BaseUrl);

			// Add authentication token if provided
			if (!string.IsNullOrEmpty(options.AuthenticationToken))
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AuthenticationToken);
			}

			client.DefaultRequestHeaders.Add("Accept", "application/json");
		})
		//Retry Policy
		.AddPolicyHandler((provider, request) =>
		{
			var options = provider.GetRequiredService<IOptions<CamundaClientOptions>>().Value;
			var logger = provider.GetRequiredService<ILogger<ICamundaHttpClient>>();
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
				.WaitAndRetryAsync(options.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
					onRetry: (outcome, retryCount, context) =>
					{	
						logger.LogWarning("Retry {RetryCount} of {PolicyKey} for {OperationKey}. Outcome: {@Outcome}", retryCount, context.PolicyKey, context.OperationKey, outcome);
					});
		})
		//Circuit Breaker Policy
		.AddPolicyHandler((provider, request) =>
		{
			var options = provider.GetRequiredService<IOptions<CamundaClientOptions>>().Value;
			var logger = provider.GetRequiredService<ILogger<ICamundaHttpClient>>();
			return Policy
				.Handle<HttpRequestException>()
				.OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError)
				.CircuitBreakerAsync(
					options.CircuitBreakerFailures,
					TimeSpan.FromSeconds(options.CircuitBreakerDurationSeconds),
					onBreak: (result, breakDelay) => logger.LogWarning("Circuit opened for {BreakDelay} seconds due to {Result}. Context: {@Context}", breakDelay.TotalSeconds, result.Exception?.Message ?? result.Result?.StatusCode.ToString(), result),
					onReset: () => logger.LogInformation("Circuit closed."),
					onHalfOpen: () => logger.LogInformation("Circuit half-open, testing recovery."));
		})
		//Timeout Policy
		.AddPolicyHandler((provider, request) =>
		{
			var options = provider.GetRequiredService<IOptions<CamundaClientOptions>>().Value;
			var logger = provider.GetRequiredService<ILogger<ICamundaHttpClient>>();
			return Policy.TimeoutAsync<HttpResponseMessage>(options.TimeoutSeconds, onTimeoutAsync: (context, timespan, task) =>
			{
				logger.LogWarning("Request timed out after {TimeoutSeconds} seconds. Context: {@Context}", timespan.TotalSeconds, context);
				return Task.CompletedTask;
			});
		});
		//.AddPolicyHandler((provider, request) =>
		//{
		//	var logger = provider.GetRequiredService<ILogger<ICamundaHttpClient>>();
		//	return Policy<HttpResponseMessage>
		//		.Handle<HttpRequestException>()
		//		.OrResult(r => !r.IsSuccessStatusCode)
		//		.RetryAsync(3, onRetry: (outcome, retryCount, context) =>
		//		{
		//			logger.LogWarning("Retry {RetryCount} for {RequestUri}. Reason: {Reason}. Context: {@Context}", retryCount, outcome.Result?.RequestMessage?.RequestUri, outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString(), outcome);
		//		});
		//});

		return services;
	}
}

