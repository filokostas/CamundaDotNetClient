using CamundaClient.Application.Interfaces;
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

// Resolve the ICamundaHttpClient
var camundaHttpClient = serviceProvider.GetRequiredService<ICamundaHttpClient>();

// Prepare request data
var variables = new Dictionary<string, object>
		{
			{ "amount", 1000 },
			{ "approved", true }
		};

try
{
	// Call StartProcessAsync
	var result = await camundaHttpClient.StartProcessAsync("ReviewInvoice", "businessKey123", variables, true);//, "businessKey123", variables, withVariablesInReturn: true);

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
