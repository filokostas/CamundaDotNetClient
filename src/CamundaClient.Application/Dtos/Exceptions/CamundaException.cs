namespace CamundaClient.Application.Dtos.Exceptions;
public record CamundaException
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

public record AuthorizationException : CamundaException
{
    public string? UserId { get; init; }
    public IEnumerable<MissingAuthorization>? MissingAuthorizations { get; init; }
}
public record MissingAuthorization
{
    public string? ResourceType { get; init; }
    public string? ResourceId { get; init; }
    public string? PermissionName { get; init; }
}

public record ParseException : CamundaException
{
    public IDictionary<string, ResourceReport>? Details { get; init; }
}

public record ResourceReport
{
    public string? ResourceName { get; init; }
    public string? ErrorMessage { get; init; }
    public string? WarningMessage { get; init; }
}