using CamundaClient.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CamundaClient.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddCamundaClient(this IServiceCollection services, string baseUrl)
    {
        services.AddHttpClient<ICamundaClient, CamundaHttpClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return services;
    }
}

