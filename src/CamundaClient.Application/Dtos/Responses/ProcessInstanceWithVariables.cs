using CamundaClient.Application.Dtos.Requests;

namespace CamundaClient.Application.Dtos.Responses;
public record ProcessInstanceWithVariables : ProcessInstance
{
    /// <summary>
    /// The variables associated with the process instance.
    /// </summary>
    public Dictionary<string, CamundaVariable>? Variables { get; init; }
}