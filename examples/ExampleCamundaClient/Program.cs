using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Interfaces.Services;
using CamundaClient.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


var services = new ServiceCollection();

// Configure logging
services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
    config.SetMinimumLevel(LogLevel.Debug);
});


services.AddCamundaHttpClient(options =>
{
    options.BaseUrl = "http://localhost:8080/engine-rest/";
    // Uncomment and add token if authentication is required
    // options.AuthenticationToken = "your-auth-token";
});

var serviceProvider = services.BuildServiceProvider();

// Resolve the ProcessDefinitionService
var processDefinitionService = serviceProvider.GetRequiredService<IProcessDefinitionService>();


// Prepare request data
var variables = new Dictionary<string, CamundaVariable>
{
    { "amount", CamundaVariable.Create(1000, "Integer") },
    { "approved", CamundaVariable.Create(true, "Boolean") }
};

// Create the StartProcessInstance request
var startProcessRequest = StartProcessInstance.Create(
    businessKey: "businessKey123",
    variables: variables,
    withVariablesInReturn: true
);

try
{
    // Call StartInstanceAsync
    var result = await processDefinitionService.StartProcessInstanceByKeyAsync("ReviewInvoice", startProcessRequest);

    Console.WriteLine($"Process started successfully with ID: {result.Id}");
    Console.WriteLine($"Definition ID: {result.DefinitionId}");

    if (result.Variables != null)
    {
        foreach (var variable in result.Variables)
        {
            Console.WriteLine($"Variable: {variable.Key}, Value: {variable.Value.Value}, Type: {variable.Value.Type}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
