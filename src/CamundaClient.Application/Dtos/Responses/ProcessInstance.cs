namespace CamundaClient.Application.Dtos.Responses;
public record ProcessInstance : Linkable
{
    /// <summary>
    /// The ID of the process instance.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// The ID of the process definition.
    /// </summary>
    public string? DefinitionId { get; init; }

    /// <summary>
    /// The business key of the process instance.
    /// </summary>
    public string? BusinessKey { get; init; }

    /// <summary>
    /// The ID of the case instance associated with the process instance.
    /// </summary>
    public string? CaseInstanceId { get; init; }

    /// <summary>
    /// A flag indicating whether the process instance is ended. Deprecated.
    /// </summary>
    public bool? Ended { get; init; }

    /// <summary>
    /// A flag indicating whether the process instance is suspended.
    /// </summary>
    public bool? Suspended { get; init; }

    /// <summary>
    /// The tenant ID of the process instance.
    /// </summary>
    public string? TenantId { get; init; }
}
