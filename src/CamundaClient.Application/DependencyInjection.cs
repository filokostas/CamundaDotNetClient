   using CamundaClient.Application.Interfaces.Services;
   using CamundaClient.Application.Services;
   using Microsoft.Extensions.DependencyInjection;

   namespace CamundaClient.Application;

   public static class DependencyInjection
   {
       public static IServiceCollection AddCamundaClientApplication(this IServiceCollection services)
       {
           services.AddScoped<IProcessDefinitionService, ProcessDefinitionService>();

           return services;
       }
   }

