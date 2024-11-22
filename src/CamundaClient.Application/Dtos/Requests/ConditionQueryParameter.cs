namespace CamundaClient.Application.Dtos.Requests;

/// <summary>
/// Base class representing a condition query parameter.
/// </summary>
public record ConditionQueryParameter
{
    /// <summary>
    /// Comparison operator to be used. <c>notLike</c> is not supported by all endpoints.
    /// </summary>
    public ComparisonOperator? Operator { get; }

    /// <summary>
    /// Variable value. Can be any value - string, number, boolean, array, or object.
    /// <para>Note: Not every endpoint supports every type.</para>
    /// </summary>
    public object? Value { get; }

    protected ConditionQueryParameter(ComparisonOperator? @operator = null, object? value = null)
    {
        Operator = @operator;
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ConditionQueryParameter"/> record.
    /// </summary>
    public static ConditionQueryParameter Create(ComparisonOperator? @operator = null, object? value = null)
    {
        return new ConditionQueryParameter(@operator, value);
    }
}