using static CamundaClient.Application.Dtos.Requests.VariableQueryParameter;

namespace CamundaClient.Application.Dtos.Requests;

/// <summary>
/// Represents a variable query parameter for filtering tasks based on variable values.
/// </summary>
public record VariableQueryParameter : ConditionQueryParameter
{
    /// <summary>
    /// Variable name.
    /// </summary>
    public string? Name { get; }

    private VariableQueryParameter(string? name = null, ComparisonOperator? @operator = null, object? value = null)
            : base(@operator, value)
    {
        Name = name;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="VariableQueryParameter"/> record.
    /// </summary>
    public static VariableQueryParameter Create(string? name = null, ComparisonOperator? @operator = null, object? value = null)
    {
        return new VariableQueryParameter(name, @operator, value);
    }
}
