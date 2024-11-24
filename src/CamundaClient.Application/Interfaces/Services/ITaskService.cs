using CamundaClient.Application.Dtos.Requests;
using CamundaClient.Application.Dtos.Responses;

namespace CamundaClient.Application.Interfaces.Services;

public interface ITaskService
{
	Task<List<CamundaTask>> QueryTasks(TaskQueryParameters? queryParameter = null, TaskQuery? taskQuery = null);
	Task<CountResult> QueryTasksCount(TaskQuery? taskQuery = null);
}
