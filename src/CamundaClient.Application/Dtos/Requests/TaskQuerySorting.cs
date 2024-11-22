using CamundaClient.Application.Utilities;

namespace CamundaClient.Application.Dtos.Requests;

/// <summary>
/// Represents sorting parameters for task queries.
/// </summary>
public record TaskQuerySorting
{
    /// <summary>
    /// Sort the results lexicographically by a given criterion.
    /// Must be used in conjunction with the sortOrder parameter.
    /// </summary>
    public SortByCriteria? SortBy { get; }

    /// <summary>
    /// Sort the results in a given order. Values may be <c>asc</c> for ascending order or <c>desc</c> for descending order.
    /// Must be used in conjunction with the sortBy parameter.
    /// </summary>
    public SortOrderCriteria? SortOrder { get; }

    /// <summary>
    /// Mandatory when <c>sortBy</c> is one of the following values: <c>processVariable</c>, <c>executionVariable</c>,
    /// <c>taskVariable</c>, <c>caseExecutionVariable</c> or <c>caseInstanceVariable</c>. Must be a JSON object with the properties
    /// <c>variable</c> and <c>type</c> where <c>variable</c> is a variable name and <c>type</c> is the name of a variable value type.
    /// </summary>
    public SortTaskQueryParameters? Parameters { get; }

    private TaskQuerySorting(SortByCriteria? sortBy = null, SortOrderCriteria? sortOrder = null, SortTaskQueryParameters? parameters = null)
    {
        SortBy = sortBy;
        SortOrder = sortOrder;
        Parameters = parameters;

        // Validation: If sortBy is one of the specified values, parameters must be provided.
        if (sortBy.HasValue && IsVariableSortBy(sortBy.Value))
        {
            Guard.NotNull(parameters, nameof(parameters), "Parameters are mandatory when sortBy is a variable-based criterion.");
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TaskQuerySorting"/> record.
    /// </summary>
    public static TaskQuerySorting Create(SortByCriteria? sortBy = null, SortOrderCriteria? sortOrder = null, SortTaskQueryParameters? parameters = null)
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

    /// <summary>
    /// Sort order criteria.
    /// </summary>
    public enum SortOrderCriteria
    {
        /// <summary>
        /// Ascending order.
        /// </summary>
        asc,

        /// <summary>
        /// Descending order.
        /// </summary>
        desc
    }
}
