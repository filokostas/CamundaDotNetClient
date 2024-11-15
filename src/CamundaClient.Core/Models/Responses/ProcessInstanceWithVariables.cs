using CamundaClient.Core.Models.Requests;

namespace CamundaClient.Core.Models.Responses;
public record ProcessInstanceWithVariables : ProcessInstance
{
    /// <summary>
    /// The variables associated with the process instance.
    /// </summary>
    public Dictionary<string, CamundaVariable>? Variables { get; init; }
}