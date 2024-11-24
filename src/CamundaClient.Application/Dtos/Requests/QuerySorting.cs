namespace CamundaClient.Application.Dtos.Requests;

public record QuerySorting<TSortByCriteria>
	where TSortByCriteria : Enum
{
	/// <summary>
	/// Sort the results by a given criterion.
	/// Must be used with the <c>SortOrder</c> parameter.
	/// </summary>
	public TSortByCriteria SortBy { get; }

	/// <summary>
	/// Sort the results in a given order: ascending or descending.
	/// Must be used with the <c>SortBy</c> parameter.
	/// </summary>
	public SortOrderCriteria SortOrder { get; }

	protected QuerySorting(TSortByCriteria sortBy, SortOrderCriteria sortOrder)
	{
		SortBy = sortBy;
		SortOrder = sortOrder;
	}

	/// <summary>
	/// Sort order criteria.
	/// </summary>
	public enum SortOrderCriteria
	{
		Asc,
		Desc
	}
}
