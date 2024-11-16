namespace CamundaClient.Application.Dtos.Responses;
public record Linkable
{
    /// <summary>
    /// The links associated with the resource.
    /// </summary>
    public List<AtomLink>? Links { get; init; }
}