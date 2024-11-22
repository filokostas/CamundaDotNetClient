namespace CamundaClient.Application.Dtos.Requests;

/// <summary>
/// Comparison operator to be used in variable queries.
/// </summary>
public enum ComparisonOperator
{
    /// <summary>
    /// Equal to.
    /// </summary>
    eq,

    /// <summary>
    /// Not equal to.
    /// </summary>
    neq,

    /// <summary>
    /// Greater than.
    /// </summary>
    gt,

    /// <summary>
    /// Greater than or equal to.
    /// </summary>
    gteq,

    /// <summary>
    /// Less than.
    /// </summary>
    lt,

    /// <summary>
    /// Less than or equal to.
    /// </summary>
    lteq,

    /// <summary>
    /// Like.
    /// </summary>
    like,

    /// <summary>
    /// Not like.
    /// </summary>
    notLike
}
