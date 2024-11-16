namespace CamundaClient.Application.Dtos.Requests;
public record StartProcessInstance
{
    public string? BusinessKey { get; }
    public Dictionary<string, CamundaVariable>? Variables { get; }
    public string? CaseInstanceId { get; }
    public List<ProcessInstanceModificationInstruction>? Instructions { get; }
    public bool? SkipCustomListeners { get; }
    public bool? SkipIoMappings { get; }
    public bool? WithVariablesInReturn { get; }

    private StartProcessInstance(string? businessKey = null, Dictionary<string, CamundaVariable>? variables = null, string? caseInstanceId = null, List<ProcessInstanceModificationInstruction>? instructions = null, bool? skipCustomListeners = null, bool? skipIoMappings = null, bool? withVariablesInReturn = null)
    {
        BusinessKey = businessKey;
        Variables = variables;
        CaseInstanceId = caseInstanceId;
        Instructions = instructions;
        SkipCustomListeners = skipCustomListeners;
        SkipIoMappings = skipIoMappings;
        WithVariablesInReturn = withVariablesInReturn;
    }

    public static StartProcessInstance Create(string? businessKey = null, Dictionary<string, CamundaVariable>? variables = null, string? caseInstanceId = null, List<ProcessInstanceModificationInstruction>? instructions = null, bool? skipCustomListeners = null, bool? skipIoMappings = null, bool? withVariablesInReturn = null)
    {
        return new StartProcessInstance(businessKey, variables, caseInstanceId, instructions, skipCustomListeners, skipIoMappings, withVariablesInReturn);
    }
}

