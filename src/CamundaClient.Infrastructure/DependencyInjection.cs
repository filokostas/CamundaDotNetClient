using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Infrastructure.Configuration;
using CamundaClient.Infrastructure.Handlers;
using CamundaClient.Infrastructure.Http;
using CamundaClient.Infrastructure.Interfaces;
using CamundaClient.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Http.Resilience;
using System.Net.Http.Headers;

namespace CamundaClient.Infrastructure;
public static class DependencyInjection
{
	/// <summary>
	/// Adds the Camunda HTTP client to the service collection with the specified configuration options.
	/// </summary>
	/// <param name="services">The service collection to add the HTTP client to.</param>
	/// <param name="configureOptions">A delegate to configure the <see cref="CamundaClientOptions"/>.</param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddCamundaHttpClient(
		this IServiceCollection services,
		Action<CamundaClientOptions> configureOptions)
	{
		services.Configure(configureOptions);

		services.AddTransient<IJsonSerializer, NewtonsoftJsonSerializer>();

		services.AddHttpClient<IHttpRequestHandler, HttpRequestHandler>((provider, client) =>
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
		.AddStandardResilienceHandler();

		services.AddTransient<IHttpResponseHandler, HttpResponseHandler>();
		services.AddTransient<ICamundaHttpClient, CamundaHttpClient>();

		// Services

		return services;
	}

}

