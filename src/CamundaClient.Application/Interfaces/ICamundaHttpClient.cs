using CamundaClient.Application.Dtos.Responses;

namespace CamundaClient.Application.Interfaces;
public interface ICamundaHttpClient
{
	Task<ProcessInstanceWithVariables> StartProcessAsync(string processDefinitionKey, string? businessKey = null, Dictionary<string, object>? variables = null, bool? withVariablesInReturn = null);
}