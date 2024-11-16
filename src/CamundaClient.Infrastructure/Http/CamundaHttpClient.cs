using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;
using CamundaClient.Application.Interfaces;
using CamundaClient.Infrastructure.Exceptions;
using CamundaClient.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace CamundaClient.Core.Services;

public class CamundaHttpClient : ICamundaHttpClient
{
    private readonly HttpClient _httpClient;
	private readonly ILogger<CamundaHttpClient> _logger;
    private readonly IJsonSerializer _jsonSerializer;

	public CamundaHttpClient(HttpClient httpClient, ILogger<CamundaHttpClient> logger, IJsonSerializer jsonSerializer)
	{
        _httpClient = httpClient;
		_logger = logger;
		_jsonSerializer = jsonSerializer;
	}

    public async Task<ProcessInstanceWithVariables> StartProcessAsync(string processDefinitionKey, string? businessKey = null, 
        Dictionary<string, object>? variables = null, bool? withVariablesInReturn = null)
    {
		_logger.LogInformation("Starting process instance with {@ProcessDefinitionKey} and {@BusinessKey}",
			processDefinitionKey, businessKey);

		// check if variables is null
		if (variables == null)
        {
            variables = new Dictionary<string, object>();
        }

		var camundaVariables = variables.ToDictionary(
            kv => kv.Key,
            kv => CamundaVariable.Create(value: kv.Value, type: GetVariableType(kv.Value))
        );

        var request = StartProcessInstance.Create(
            businessKey: businessKey,
            variables: camundaVariables,
            caseInstanceId: null,
            instructions: null,
            skipCustomListeners: false,
            skipIoMappings: false,
            withVariablesInReturn: withVariablesInReturn
        );

		if (request == null)
		{
			throw new InvalidOperationException("Request cannot be null.");
		}

		var jsonContent = _jsonSerializer.Serialize(request);

		if (string.IsNullOrEmpty(jsonContent))
		{
			throw new InvalidOperationException("Serialized content cannot be null or empty.");
		}

		var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

		_logger.LogDebug("Sending HTTP POST to /process-definition/key/{@DefinitionKey}/start", processDefinitionKey);

		// Αποστολή του αιτήματος στο Camunda API
		var response = await _httpClient.PostAsync($"process-definition/key/{processDefinitionKey}/start", httpContent);


		if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
			_logger.LogError("Failed to start process instance. StatusCode: {@StatusCode}, Response: {@Response}",
			   response.StatusCode, errorContent);

			CamundaError? errorDetails = null;

            // Check if the status code is 400 or 500 before attempting to deserialize error details
            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.InternalServerError)
            {
                try
                {
                    errorDetails = _jsonSerializer.Deserialize<CamundaError>(errorContent);
                }
                catch
                {
                }
            }

            throw new CamundaApiException($"Camunda API returned an error. Status code: {response.StatusCode}",
                                       (int)response.StatusCode,
                                       errorDetails);
        }

        var responseData = await response.Content.ReadAsStringAsync();

		_logger.LogInformation("Process instance started successfully. Response: {@Response}", responseData);

		var processInstance = _jsonSerializer.Deserialize<ProcessInstanceWithVariables>(responseData);

        return processInstance;
    }

    private string GetVariableType(object value)
    {
        return value switch
        {
            int => "Integer",
            bool => "Boolean",
            string => "String",
            double => "Double",
            long => "Long",
            _ => "Object"
        };
    }
}

