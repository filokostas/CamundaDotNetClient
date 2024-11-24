using CamundaClient.Application.Dtos.Requests;

namespace CamundaClient.Application.Dtos.Responses;

/// <summary>
/// Represents a task in Camunda.
/// </summary>
public record CamundaTask
{
	/// <summary>
	/// The task id.
	/// </summary>
	public string? Id { get; init; }

	/// <summary>
	/// The task name.
	/// </summary>
	public string? Name { get; init; }

	/// <summary>
	/// The assignee's id.
	/// </summary>
	public string? Assignee { get; init; }

	/// <summary>
	/// The owner's id.
	/// </summary>
	public string? Owner { get; init; }

	/// <summary>
	/// The date the task was created on.
	/// </summary>
	public DateTime? Created { get; init; }

	/// <summary>
	/// The date the task was last updated.
	/// </summary>
	public DateTime? LastUpdated { get; init; }

	/// <summary>
	/// The task's due date.
	/// </summary>
	public DateTime? Due { get; init; }

	/// <summary>
	/// The follow-up date for the task.
	/// </summary>
	public DateTime? FollowUp { get; init; }

	/// <summary>
	/// The task's delegation state. Possible values are `PENDING` and `RESOLVED`.
	/// </summary>
	public DelegationStateType? DelegationState { get; init; }

	/// <summary>
	/// The task's description.
	/// </summary>
	public string? Description { get; init; }

	/// <summary>
	/// The id of the execution the task belongs to.
	/// </summary>
	public string? ExecutionId { get; init; }

	/// <summary>
	/// The id of the parent task, if this task is a subtask.
	/// </summary>
	public string? ParentTaskId { get; init; }

	/// <summary>
	/// The task's priority.
	/// </summary>
	public int? Priority { get; init; }

	/// <summary>
	/// The id of the process definition the task belongs to.
	/// </summary>
	public string? ProcessDefinitionId { get; init; }

	/// <summary>
	/// The id of the process instance the task belongs to.
	/// </summary>
	public string? ProcessInstanceId { get; init; }

	/// <summary>
	/// The id of the case execution the task belongs to.
	/// </summary>
	public string? CaseExecutionId { get; init; }

	/// <summary>
	/// The id of the case definition the task belongs to.
	/// </summary>
	public string? CaseDefinitionId { get; init; }

	/// <summary>
	/// The id of the case instance the task belongs to.
	/// </summary>
	public string? CaseInstanceId { get; init; }

	/// <summary>
	/// The task's key.
	/// </summary>
	public string? TaskDefinitionKey { get; init; }

	/// <summary>
	/// Whether the task belongs to a process instance that is suspended.
	/// </summary>
	public bool? Suspended { get; init; }

	/// <summary>
	/// If not `null`, the form key for the task.
	/// </summary>
	public string? FormKey { get; init; }

	/// <summary>
	/// The Camunda form reference.
	/// </summary>
	public CamundaFormRef? CamundaFormRef { get; init; }

	/// <summary>
	/// If not `null`, the tenant id of the task.
	/// </summary>
	public string? TenantId { get; init; }
}
