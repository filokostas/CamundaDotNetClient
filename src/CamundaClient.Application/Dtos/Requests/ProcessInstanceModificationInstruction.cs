using CamundaClient.Application.Utilities;

namespace CamundaClient.Application.Dtos.Requests;

public record ProcessInstanceModificationInstruction
{
    public InstructionType Type { get; }
    public Dictionary<string, CamundaVariable>? Variables { get; }
    public string? ActivityId { get; }
    public string? TransitionId { get; }
    public string? ActivityInstanceId { get; }
    public string? TransitionInstanceId { get; }
    public string? AncestorActivityInstanceId { get; }
    public bool? CancelCurrentActiveActivityInstances { get; }

    private ProcessInstanceModificationInstruction(InstructionType type, Dictionary<string, CamundaVariable>? variables = null, string? activityId = null, string? transitionId = null, string? activityInstanceId = null, string? transitionInstanceId = null, string? ancestorActivityInstanceId = null, bool? cancelCurrentActiveActivityInstances = null)
    {
        Guard.NotNull<InstructionType>(type, nameof(type));

        Type = type;
        Variables = variables;
        ActivityId = activityId;
        TransitionId = transitionId;
        ActivityInstanceId = activityInstanceId;
        TransitionInstanceId = transitionInstanceId;
        AncestorActivityInstanceId = ancestorActivityInstanceId;
        CancelCurrentActiveActivityInstances = cancelCurrentActiveActivityInstances;
    }

    public static ProcessInstanceModificationInstruction Create(InstructionType type, Dictionary<string, CamundaVariable>? variables = null, string? activityId = null, string? transitionId = null, string? activityInstanceId = null, string? transitionInstanceId = null, string? ancestorActivityInstanceId = null, bool? cancelCurrentActiveActivityInstances = null)
    {
        return new ProcessInstanceModificationInstruction(type, variables, activityId, transitionId, activityInstanceId, transitionInstanceId, ancestorActivityInstanceId, cancelCurrentActiveActivityInstances);
    }
}

