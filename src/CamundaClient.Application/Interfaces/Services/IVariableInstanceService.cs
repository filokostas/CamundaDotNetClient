using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;

namespace CamundaClient.Application.Interfaces.Services;

public interface IVariableInstanceService
{
	Task<List<VariableInstance>> QueryVariableInstances(VariableInstanceQueryParameters? queryParameter = null, VariableInstanceQuery? variableInstanceQuery = null);
	Task<CountResult> QueryVariableInstancesCount(VariableInstanceQuery? variableInstanceQuery = null);
}
