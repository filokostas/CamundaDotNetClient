using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;
using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Application.Interfaces.Services;

namespace CamundaClient.Application.Services;

public class ProcessDefinitionService : IProcessDefinitionService
{
    private readonly ICamundaHttpClient _httpClient;

    public ProcessDefinitionService(ICamundaHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProcessInstanceWithVariables> StartProcessInstanceByKeyAsync(
        string processDefinitionKey,
        StartProcessInstance request)
    {
        //if (request == null)
        //{
        //    throw new ArgumentNullException(nameof(request), "The request object cannot be null.");
        //}

        return await _httpClient.PostAsync<ProcessInstanceWithVariables>(
            $"process-definition/key/{processDefinitionKey}/start",
            request
        );
    }
}
