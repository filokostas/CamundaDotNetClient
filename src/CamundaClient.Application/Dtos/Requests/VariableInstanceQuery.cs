namespace CamundaClient.Application.Dtos.Requests;
/// <summary>
/// A variable instance query which defines a list of variable instances
/// </summary>
public record VariableInstanceQuery
{
	/// <summary>
	/// Filter by variable instance name.
	/// </summary>
	public string? VariableName { get; }
	/// <summary>
	/// Filter by the variable instance name. The parameter can include the wildcard `%` to
	/// express like-strategy such as: starts with (`%`name), ends with (name`%`) or
	/// contains (`%`name`%`).
	/// </summary>
	public string? VariableNameLike { get; }
	/// <summary>
	/// Only include variable instances which belong to one of the passed \nprocess instance ids.
	/// </summary>
	public IEnumerable<string>? ProcessInstanceIdIn { get; }
	/// <summary>
	/// Only include variable instances which belong to one of the passed 
	/// execution ids.
	/// </summary>
	public IEnumerable<string>? ExecutionIdIn { get; }
	/// <summary>
	/// Only include variable instances which belong to one of the passed  case instance ids.
	/// </summary>
	public IEnumerable<string>? CaseInstanceIdIn { get; }
	/// <summary>
	/// Only include variable instances which belong to one of the passed  case execution ids.
	/// </summary>
	public IEnumerable<string>? CaseExecutionIdIn { get; }
	/// <summary>
	/// Only include variable instances which belong to one of the passed  task
	/// ids.
	/// </summary>
	public IEnumerable<string>? TaskIdIn { get; }
	/// <summary>
	/// Only include variable instances which belong to one of the passed 
	/// batch ids.
	/// </summary>
	public IEnumerable<string>? BatchIdIn { get; }
	/// <summary>
	/// Only include variable instances which belong to one of the passed 
	/// activity instance ids.
	/// </summary>
	public IEnumerable<string>? ActivityInstanceIdIn { get; }
	/// <summary>
	/// Only include variable instances which belong to one of the passed tenant ids.
	/// </summary>
	public IEnumerable<string>? TenantIdIn { get; }
	/// <summary>
	/// An array to only include variable instances that have the certain values.
	/// The array consists of objects with the three properties `name`, `operator` and `value`. `name (String)` is the
	/// variable name, `operator (String)` is the comparison operator to be used and `value` the variable value.
	/// `value` may be `String`, `Number` or `Boolean`.
	/// Valid operator values are: `eq` - equal to; `neq` - not equal to; `gt` - greater than; `gteq` - greater
	/// than or equal to; `lt` - lower than; `lteq` - lower than or equal to; `like`
	/// </summary>
	public IEnumerable<VariableQueryParameter>? VariableValues { get; }
	/// <summary>
	/// Match all variable names provided in `variableValues` case-insensitively. If set to `true`
	/// **variableName** and **variablename** are treated as equal.
	/// </summary>
	public bool? VariableNamesIgnoreCase { get; }
	/// <summary>
	/// Match all variable values provided in `variableValues` case-insensitively. If set to
	/// `true` **variableValue** and **variablevalue** are treated as equal.
	/// </summary>
	public bool? VariableValuesIgnoreCase { get; }
	/// <summary>
	/// Only include variable instances which belong to one of passed scope ids.
	/// </summary>
	public IEnumerable<string>? VariableScopeIdIn { get; }

	// sorting
	/// <summary>
	/// Sort the results lexicographically by a given criterion. Must be used in conjunction with the `sortOrder` parameter.
	/// </summary>
	public VariableInstanceQuerySorting? SortBy { get; }

	/// <summary>
	/// Creates a new instance of the <see cref="VariableInstanceQuery"/> record.
	/// </summary>
	private VariableInstanceQuery(
		string? variableName = null,
		string? variableNameLike = null,
		IEnumerable<string>? processInstanceIdIn = null,
		IEnumerable<string>? executionIdIn = null,
		IEnumerable<string>? caseInstanceIdIn = null,
		IEnumerable<string>? caseExecutionIdIn = null,
		IEnumerable<string>? taskIdIn = null,
		IEnumerable<string>? batchIdIn = null,
		IEnumerable<string>? activityInstanceIdIn = null,
		IEnumerable<string>? tenantIdIn = null,
		IEnumerable<VariableQueryParameter>? variableValues = null,
		bool? variableNamesIgnoreCase = null,
		bool? variableValuesIgnoreCase = null,
		IEnumerable<string>? variableScopeIdIn = null,
		VariableInstanceQuerySorting? sortBy = null)
	{
		VariableName = variableName;
		VariableNameLike = variableNameLike;
		ProcessInstanceIdIn = processInstanceIdIn;
		ExecutionIdIn = executionIdIn;
		CaseInstanceIdIn = caseInstanceIdIn;
		CaseExecutionIdIn = caseExecutionIdIn;
		TaskIdIn = taskIdIn;
		BatchIdIn = batchIdIn;
		ActivityInstanceIdIn = activityInstanceIdIn;
		TenantIdIn = tenantIdIn;
		VariableValues = variableValues;
		VariableNamesIgnoreCase = variableNamesIgnoreCase;
		VariableValuesIgnoreCase = variableValuesIgnoreCase;
		VariableScopeIdIn = variableScopeIdIn;
		SortBy = sortBy;
	}

	/// <summary>
	/// Creates a new instance of the <see cref="VariableInstanceQuery"/> record.
	/// </summary>
	public static VariableInstanceQuery Create(
		string? variableName = null,
		string? variableNameLike = null,
		IEnumerable<string>? processInstanceIdIn = null,
		IEnumerable<string>? executionIdIn = null,
		IEnumerable<string>? caseInstanceIdIn = null,
		IEnumerable<string>? caseExecutionIdIn = null,
		IEnumerable<string>? taskIdIn = null,
		IEnumerable<string>? batchIdIn = null,
		IEnumerable<string>? activityInstanceIdIn = null,
		IEnumerable<string>? tenantIdIn = null,
		IEnumerable<VariableQueryParameter>? variableValues = null,
		bool? variableNamesIgnoreCase = null,
		bool? variableValuesIgnoreCase = null,
		IEnumerable<string>? variableScopeIdIn = null,
		VariableInstanceQuerySorting? sortBy = null)
	{
		return new VariableInstanceQuery(
			variableName,
			variableNameLike,
			processInstanceIdIn,
			executionIdIn,
			caseInstanceIdIn,
			caseExecutionIdIn,
			taskIdIn,
			batchIdIn,
			activityInstanceIdIn,
			tenantIdIn,
			variableValues,
			variableNamesIgnoreCase,
			variableValuesIgnoreCase,
			variableScopeIdIn,
			sortBy);
	}
}
