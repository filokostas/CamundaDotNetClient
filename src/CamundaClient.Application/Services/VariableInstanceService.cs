using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;
using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Application.Interfaces.Services;

namespace CamundaClient.Application.Services;

public class VariableInstanceService(ICamundaHttpClient httpClient) : IVariableInstanceService
{
	public async Task<List<VariableInstance>> QueryVariableInstances(VariableInstanceQueryParameters? queryParameter = null, VariableInstanceQuery? variableInstanceQuery = null)
	{
		var queryParams = new List<string>();
		// check if queryParameter is not null
		if (queryParameter is not null)
		{
			if (queryParameter.FirstResult.HasValue && queryParameter.MaxResults.HasValue)
			{
				queryParams.Add($"firstResult={queryParameter.FirstResult.Value}");
				queryParams.Add($"maxResults={queryParameter.MaxResults.Value}");
			}

			queryParams.Add($"deserializeValues={queryParameter.DeserializeValues}");
		}

		var queryString = string.Join("&", queryParams);
		var endpoint = string.IsNullOrEmpty(queryString) ? "variable-instance" : $"variable-instance?{queryString}";
		return await httpClient.PostAsync<List<VariableInstance>>(endpoint, variableInstanceQuery);
	}

	public async Task<CountResult> QueryVariableInstancesCount(VariableInstanceQuery? variableInstanceQuery = null)
	{
		var endpoint = "variable-instance/count";
		return await httpClient.PostAsync<CountResult>(endpoint, variableInstanceQuery);
	}
}
