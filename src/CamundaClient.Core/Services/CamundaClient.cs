using CamundaClient.Core.Exceptions;
using CamundaClient.Core.Models.Requests;
using CamundaClient.Core.Models.Responses;
using CamundaClient.Core.Services.Interfaces;
using CamundaClient.Core.Utilities;
using System.Text;

namespace CamundaClient.Core.Services;

public class CamundaClient : ICamundaClient
{
    private readonly HttpClient _httpClient;

    public CamundaClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> StartProcessAsync(string processDefinitionKey, string businessKey, Dictionary<string, object> variables, bool withVariablesInReturn = false)
    {
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

        var jsonContent = JsonHelper.Serialize(request);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Αποστολή του αιτήματος στο Camunda API
        var response = await _httpClient.PostAsync($"/process-definition/key/{processDefinitionKey}/start", httpContent);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            CamundaError? errorDetails = null;

            try
            {
                errorDetails = JsonHelper.Deserialize<CamundaError>(errorContent);
            }
            catch
            {
            }

            throw new CamundaApiException($"Camunda API returned an error. Status code: {response.StatusCode}",
                                       (int)response.StatusCode,
                                       errorDetails);
        }

        var responseData = await response.Content.ReadAsStringAsync();
        var processInstance = JsonHelper.Deserialize<ProcessInstanceWithVariables>(responseData);

        return processInstance.Id;
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

