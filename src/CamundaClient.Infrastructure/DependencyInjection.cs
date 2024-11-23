using CamundaClient.Application;
using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Infrastructure.Configuration;
using CamundaClient.Infrastructure.Handlers;
using CamundaClient.Infrastructure.Http;
using CamundaClient.Infrastructure.Interfaces;
using CamundaClient.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        // Register CamundaDateTimeConverter
        services.AddSingleton<JsonConverter, CamundaDateTimeConverter>();
		services.AddSingleton<JsonConverter, CamundaVariableConverter>();
		services.AddSingleton<JsonConverter, StringEnumConverter>();

		// Register JsonSerializerSettingsConfig
		services.AddSingleton<JsonSerializerSettingsConfig>(sp =>
		{
			var converters = sp.GetServices<JsonConverter>();
			return new JsonSerializerSettingsConfig(converters);
		});

		// Register IJsonSerializer with injected JsonSerializerSettings
		services.AddTransient<IJsonSerializer>(sp =>
		{
			var settingsConfig = sp.GetRequiredService<JsonSerializerSettingsConfig>();
			return new NewtonsoftJsonSerializer(settingsConfig.Settings);
		});

		//     // Register JsonSerializerSettings
		//     services.AddSingleton<JsonSerializerSettings>(sp =>
		//     {
		//         var settings = new JsonSerializerSettings
		//         {
		//             ContractResolver = new CamelCasePropertyNamesContractResolver(),
		//             NullValueHandling = NullValueHandling.Ignore,
		//             Formatting = Formatting.Indented,
		//             DateTimeZoneHandling = DateTimeZoneHandling.Local,
		//             DateFormatHandling = DateFormatHandling.IsoDateFormat,
		//	DateParseHandling = DateParseHandling.None, // Prevent automatic date parsing
		//};

		//         // Get all registered JsonConverters
		//         var converters = sp.GetServices<JsonConverter>();

		//         // Add converters to the settings
		//         foreach (var converter in converters)
		//         {
		//             settings.Converters.Add(converter);
		//         }

		//         return settings;
		//     });

		//// Register IJsonSerializer with injected JsonSerializerSettings
		//services.AddTransient<IJsonSerializer>(sp =>
  //      {
  //          var settings = sp.GetRequiredService<JsonSerializerSettings>();
  //          return new NewtonsoftJsonSerializer(settings);
  //      });

        services.AddTransient<IHttpRequestHandler, HttpRequestHandler>();
        services.AddTransient<IHttpResponseHandler, HttpResponseHandler>();

        services.AddHttpClient<ICamundaHttpClient, CamundaHttpClient>()
            .ConfigureHttpClient((provider, client) =>
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


        services.AddCamundaClientApplication();

        return services;
    }

}

