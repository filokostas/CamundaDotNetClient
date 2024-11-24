using CamundaClient.Application.Utilities;

namespace CamundaClient.Application.Dtos.Requests;

/// <summary>
/// Represents sorting parameters for task queries.
/// </summary>
public record TaskQuerySorting : QuerySorting<TaskQuerySorting.SortByCriteria>
{
    /// <summary>
    /// Mandatory when <c>sortBy</c> is one of the following values: <c>processVariable</c>, <c>executionVariable</c>,
    /// <c>taskVariable</c>, <c>caseExecutionVariable</c> or <c>caseInstanceVariable</c>. Must be a JSON object with the properties
    /// <c>variable</c> and <c>type</c> where <c>variable</c> is a variable name and <c>type</c> is the name of a variable value type.
    /// </summary>
    public SortTaskQueryParameters? Parameters { get; }

    private TaskQuerySorting(
        SortByCriteria sortBy, 
        SortOrderCriteria sortOrder, 
        SortTaskQueryParameters? parameters = null) : base(sortBy, sortOrder)
	{
        Parameters = parameters;

        // Validation: If sortBy is one of the specified values, parameters must be provided.
        if (IsVariableSortBy(sortBy))
        {
            Guard.NotNull(parameters, nameof(parameters), "Parameters are mandatory when sortBy is a variable-based criterion.");
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TaskQuerySorting"/> record.
    /// </summary>
    public static TaskQuerySorting Create(
        SortByCriteria sortBy, 
        SortOrderCriteria sortOrder, 
        SortTaskQueryParameters? parameters = null)
    {
        return new TaskQuerySorting(sortBy, sortOrder, parameters);
    }



    // Helper method to check if sortBy is one of the variable-based criteria
    private static bool IsVariableSortBy(SortByCriteria sortBy)
    {
        return sortBy == SortByCriteria.processVariable ||
               sortBy == SortByCriteria.executionVariable ||
               sortBy == SortByCriteria.taskVariable ||
               sortBy == SortByCriteria.caseExecutionVariable ||
               sortBy == SortByCriteria.caseInstanceVariable;
    }

    /// <summary>
    /// Sort criteria for task queries.
    /// </summary>
    public enum SortByCriteria
    {
        instanceId,
        caseInstanceId,
        dueDate,
        executionId,
        caseExecutionId,
        assignee,
        created,
        lastUpdated,
        followUpDate,
        description,
        id,
        name,
        nameCaseInsensitive,
        priority,
        processVariable,
        executionVariable,
        taskVariable,
        caseExecutionVariable,
        caseInstanceVariable
    }
}
