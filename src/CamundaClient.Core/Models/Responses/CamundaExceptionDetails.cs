namespace CamundaClient.Core.Models.Responses;
public record CamundaError
{
    /// <summary>
    /// An exception class indicating the occurred error.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// A detailed message of the error.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// The code allows identifying the error programmatically.
    /// </summary>
    public int? Code { get; init; }
}
