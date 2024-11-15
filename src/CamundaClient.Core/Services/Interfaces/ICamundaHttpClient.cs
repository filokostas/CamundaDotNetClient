using CamundaClient.Core.Models.Responses;

namespace CamundaClient.Core.Services.Interfaces;
public interface ICamundaHttpClient
{
    Task<ProcessInstanceWithVariables> StartProcessAsync(string processDefinitionKey, string? businessKey = null, Dictionary<string, object>? variables = null, bool? withVariablesInReturn = null);
}