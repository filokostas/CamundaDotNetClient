namespace CamundaClient.Application.Dtos.Responses;
public record AtomLink
{
    /// <summary>
    /// The HTTP method for the link.
    /// </summary>
    public string? Method { get; init; }

    /// <summary>
    /// The URL for the link.
    /// </summary>
    public string? Href { get; init; }

    /// <summary>
    /// The relation type of the link.
    /// </summary>
    public string? Rel { get; init; }
}
