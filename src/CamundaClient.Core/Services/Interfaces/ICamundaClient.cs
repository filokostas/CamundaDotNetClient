namespace CamundaClient.Core.Services.Interfaces;
public interface ICamundaClient
{
    Task<string> StartProcessAsync(string processDefinitionKey, string businessKey, Dictionary<string, object> variables, bool withVariablesInReturn = false);
}