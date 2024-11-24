using CamundaClient.Application.Dtos.Requests;

namespace CamundaClient.Application.Dtos.Responses;

public record VariableInstance
{
	public required object Value { get; init; }
	public string? Type { get; init; }
	public Dictionary<string, object>? ValueInfo { get; init; }
	public bool? Local { get; init; }
	/// <summary>
	/// The id of the variable instance.
	/// </summary>
	public string? Id { get; init; }
	/// <summary>
	/// The name of the variable instance.
	/// </summary>
	public string? Name { get; init; }
	/// <summary>
	/// The id of the process definition that this variable instance belongs to.
	/// </summary>
	public string? ProcessDefinitionId { get; init; }
	/// <summary>
	/// The id of the process instance that this variable instance belongs to.
	/// </summary>
	public string? ProcessInstanceId { get; init; }
	/// <summary>
	/// The id of the execution that this variable instance belongs to.
	/// </summary>
	public string? ExecutionId { get; init; }
	/// <summary>
	/// The id of the case instance that this variable instance belongs to.
	/// </summary>
	public string? CaseInstanceId { get; init; }
	/// <summary>
	/// The id of the case execution that this variable instance belongs to.
	/// </summary>
	public string? CaseExecutionId { get; init; }
	/// <summary>
	/// The id of the task that this variable instance belongs to.
	/// </summary>
	public string? TaskId { get; init; }
	/// <summary>
	/// The id of the batch that this variable instance belongs to.
	/// </summary>
	public string? BatchId { get; init; }
	/// <summary>
	/// The id of the activity instance that this variable instance belongs to.
	/// </summary>
	public string? ActivityInstanceId { get; init; }
	/// <summary>
	/// The id of the tenant that this variable instance belongs to.
	/// </summary>
	public string? TenantId { get; init; }
	/// <summary>
	/// An error message in case a Java Serialized Object could not be de-serialized.
	/// </summary>
	public string? ErrorMessage { get; init; }
}


