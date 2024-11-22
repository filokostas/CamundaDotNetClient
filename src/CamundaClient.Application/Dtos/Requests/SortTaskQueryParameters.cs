namespace CamundaClient.Application.Dtos.Requests;

/// <summary>
/// Parameters for sorting task queries by variable values.
/// </summary>
public record SortTaskQueryParameters
{
    /// <summary>
    /// The name of the variable to sort by.
    /// </summary>
    public string? Variable { get; }

    /// <summary>
    /// The name of the type of the variable value.
    /// </summary>
    public string? Type { get; }

    private SortTaskQueryParameters(string? variable = null, string? type = null)
    {
        Variable = variable;
        Type = type;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SortTaskQueryParameters"/> record.
    /// </summary>
    public static SortTaskQueryParameters Create(string? variable = null, string? type = null)
    {
        return new SortTaskQueryParameters(variable, type);
    }
}
