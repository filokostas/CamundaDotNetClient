using CamundaClient.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using CamundaClient.Core.Services.Interfaces;
using CamundaClient.Core.Services;
using System.Net.Http.Headers;
using Polly.Extensions.Http;
using Polly;
using System.Net;

namespace CamundaClient.Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddCamundaHttpClient(this IServiceCollection services, Action<CamundaClientOptions> configureOptions)
	{
		services.Configure(configureOptions);

		var retryPolicy = HttpPolicyExtensions
			.HandleTransientHttpError()
			.OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
			.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

		// Timeout Policy
		var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10); // 10 seconds timeout

		// Circuit Breaker Policy
		var circuitBreakerPolicy = Policy
			.Handle<HttpRequestException>()
			.OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError)
			.CircuitBreakerAsync(2, TimeSpan.FromSeconds(30)); // 2 αποτυχίες πριν το circuit ανοίξει


		services.AddHttpClient<ICamundaHttpClient, CamundaHttpClient>((provider, client) =>
		{
			var options = provider.GetRequiredService<IOptions<CamundaClientOptions>>().Value;

			client.BaseAddress = new Uri(options.BaseUrl);

			// Add authentication token if provided
			if (!string.IsNullOrEmpty(options.AuthenticationToken))
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AuthenticationToken);
			}

			client.DefaultRequestHeaders.Add("Accept", "application/json");
		})
		.AddPolicyHandler(retryPolicy)
		.AddPolicyHandler(circuitBreakerPolicy)
		.AddPolicyHandler(timeoutPolicy);

		return services;
	}
}

