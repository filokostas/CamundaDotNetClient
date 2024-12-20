using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;
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
var taskService = serviceProvider.GetRequiredService<ITaskService>();


// Prepare request data
var variables = new Dictionary<string, CamundaVariable>
{
    { "amount", CamundaVariable.Create(1000, "Integer") },
    { "approved", CamundaVariable.Create(true, "Boolean") }
};

// Create the StartProcessInstance request
var startProcessRequest = StartProcessInstance.Create(
    businessKey: "businessKey124",
    variables: variables,
    withVariablesInReturn: true
);

ProcessInstanceWithVariables processInstanceWithVariables = null;

try
{
    // Call StartInstanceAsync
    processInstanceWithVariables = await processDefinitionService.StartProcessInstanceByKeyAsync("ReviewInvoice", startProcessRequest);

    Console.WriteLine($"Process started successfully with ID: {processInstanceWithVariables.Id}");
    Console.WriteLine($"Definition ID: {processInstanceWithVariables.DefinitionId}");

    if (processInstanceWithVariables.Variables != null)
    {
        foreach (var variable in processInstanceWithVariables.Variables)
        {
            Console.WriteLine($"Variable: {variable.Key}, Value: {variable.Value.Value}, Type: {variable.Value.Type}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

int FirstResult = 0;
int MaxResults = 10;
var queryParameter = TaskQueryParameter.Create(FirstResult, MaxResults);

var taskQuery = TaskQuery.Create(processInstanceId: processInstanceWithVariables!.Id);

try
{
    var result = await taskService.QueryTasks(queryParameter, taskQuery);

    Console.WriteLine(result.Count);

	foreach (var camundaTask in result)
    {
        Console.WriteLine($"Task ID: {camundaTask.Id}");
	}
}
catch (Exception ex)
{
	Console.WriteLine($"Error: {ex.Message}");
}
