using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;

namespace CamundaClient.Application.Interfaces.Services;
public interface IProcessDefinitionService
{
	Task<ProcessInstanceWithVariables> StartProcessInstanceByKeyAsync(
		string processDefinitionKey,
		StartProcessInstance request);
}
