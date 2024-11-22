namespace CamundaClient.Application.Dtos.Requests;

/// <summary>
/// Possible delegation states for a task.
/// </summary>
public enum DelegationStateType
{
    /// <summary>
    /// CamundaTask is pending delegation.
    /// </summary>
    PENDING,

    /// <summary>
    /// CamundaTask delegation is resolved.
    /// </summary>
    RESOLVED
}
