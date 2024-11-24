namespace CamundaClient.Application.Dtos.Requests;

public record VariableInstanceQuerySorting : QuerySorting<VariableInstanceQuerySorting.SortByCriteria>
{
	private VariableInstanceQuerySorting(SortByCriteria sortBy, SortOrderCriteria sortOrder) : base(sortBy, sortOrder)
	{
	}
	/// <summary>
	/// Creates a new instance of the <see cref="VariableInstanceQuerySorting"/> record.
	/// </summary>
	public static VariableInstanceQuerySorting Create(SortByCriteria sortBy, SortOrderCriteria sortOrder)
	{
		return new VariableInstanceQuerySorting(sortBy, sortOrder);
	}
	/// <summary>
	/// Sort criteria for variable queries.
	/// </summary>
	public enum SortByCriteria
	{
		variableName,
		variableType,
		activityInstanceId,
		tenantId
	}
}
