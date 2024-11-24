using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;
using CamundaClient.Application.Interfaces.Http;
using CamundaClient.Application.Interfaces.Services;

namespace CamundaClient.Application.Services;
public class TaskService(ICamundaHttpClient httpClient) : ITaskService
{
	public async Task<List<CamundaTask>> QueryTasks(TaskQueryParameters? queryParameter = null, TaskQuery? taskQuery = null)
	{
		var queryParams = new List<string>();

		// check if queryParameter is not null
		if (queryParameter is not null)
		{
			// add firstResult and maxResults to queryParams
			queryParams.Add($"firstResult={queryParameter.FirstResult.Value}");
			queryParams.Add($"maxResults={queryParameter.MaxResults.Value}");
		}

		var queryString = string.Join("&", queryParams);
		var endpoint = string.IsNullOrEmpty(queryString) ? "task" : $"task?{queryString}";

		return await httpClient.PostAsync<List<CamundaTask>>(endpoint, taskQuery);
	}

	public async Task<CountResult> QueryTasksCount(TaskQuery? taskQuery = null)
	{
		var endpoint = "task/count";
		return await httpClient.PostAsync<CountResult>(endpoint, taskQuery);
	}
}
