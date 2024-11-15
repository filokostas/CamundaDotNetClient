namespace CamundaClient.Core.Models.Responses;
public record Linkable
{
    /// <summary>
    /// The links associated with the resource.
    /// </summary>
    public List<AtomLink>? Links { get; init; }
}