using CamundaClient.Application.Utilities;

namespace CamundaClient.Application.Dtos.Requests;

/// <summary>
/// A Task query which defines a group of Tasks.
/// </summary>
public record TaskQuery
{
    /// <summary>
    /// Restrict to task with the given id.
    /// </summary>
    public string? TaskId { get; }

    /// <summary>
    /// Restrict to tasks with any of the given ids.
    /// </summary>
    public List<string>? TaskIdIn { get; }

    /// <summary>
    /// Restrict to tasks that belong to process instances with the given id.
    /// </summary>
    public string? ProcessInstanceId { get; }

    /// <summary>
    /// Restrict to tasks that belong to process instances with the given ids.
    /// </summary>
    public List<string>? ProcessInstanceIdIn { get; }

    /// <summary>
    /// Restrict to tasks that belong to process instances with the given business key.
    /// </summary>
    public string? ProcessInstanceBusinessKey { get; }

    /// <summary>
    /// Restrict to tasks that belong to process instances with the given business key which 
    /// is described by an expression. See the 
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? ProcessInstanceBusinessKeyExpression { get; }

    /// <summary>
    /// Restrict to tasks that belong to process instances with one of the given business keys. 
    /// The keys need to be in a comma-separated list.
    /// </summary>
    public List<string>? ProcessInstanceBusinessKeyIn { get; }

    /// <summary>
    /// Restrict to tasks that have a process instance business key that has the parameter 
    /// value as a substring.
    /// </summary>
    public string? ProcessInstanceBusinessKeyLike { get; }

    /// <summary>
    /// Restrict to tasks that have a process instance business key that has the parameter 
    /// value as a substring and is described by an expression. See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? ProcessInstanceBusinessKeyLikeExpression { get; }

    /// <summary>
    /// Restrict to tasks that belong to a process definition with the given id.
    /// </summary>
    public string? ProcessDefinitionId { get; }

    /// <summary>
    /// Restrict to tasks that belong to a process definition with the given key.
    /// </summary>
    public string? ProcessDefinitionKey { get; }

    /// <summary>
    /// Restrict to tasks that belong to a process definition with one of the given keys. The 
    /// keys need to be in a comma-separated list.
    /// </summary>
    public List<string>? ProcessDefinitionKeyIn { get; }

    /// <summary>
    /// Restrict to tasks that belong to a process definition with the given name.
    /// </summary>
    public string? ProcessDefinitionName { get; }

    /// <summary>
    /// Restrict to tasks that have a process definition name that has the parameter value as 
    /// a substring.
    /// </summary>
    public string? ProcessDefinitionNameLike { get; }

    /// <summary>
    /// Restrict to tasks that belong to an execution with the given id.
    /// </summary>
    public string? ExecutionId { get; }

    /// <summary>
    /// Restrict to tasks that belong to case instances with the given id.
    /// </summary>
    public string? CaseInstanceId { get; }

    /// <summary>
    /// Restrict to tasks that belong to case instances with the given business key.
    /// </summary>
    public string? CaseInstanceBusinessKey { get; }

    /// <summary>
    /// Restrict to tasks that have a case instance business key that has the parameter value 
    /// as a substring.
    /// </summary>
    public string? CaseInstanceBusinessKeyLike { get; }

    /// <summary>
    /// Restrict to tasks that belong to a case definition with the given id.
    /// </summary>
    public string? CaseDefinitionId { get; }

    /// <summary>
    /// Restrict to tasks that belong to a case definition with the given key.
    /// </summary>
    public string? CaseDefinitionKey { get; }

    /// <summary>
    /// Restrict to tasks that belong to a case definition with the given name.
    /// </summary>
    public string? CaseDefinitionName { get; }

    /// <summary>
    /// Restrict to tasks that have a case definition name that has the parameter value as a 
    /// substring.
    /// </summary>
    public string? CaseDefinitionNameLike { get; }

    /// <summary>
    /// Restrict to tasks that belong to a case execution with the given id.
    /// </summary>
    public string? CaseExecutionId { get; }

    /// <summary>
    /// Only include tasks which belong to one of the passed and comma-separated activity 
    /// instance ids.
    /// </summary>
    public List<string>? ActivityInstanceIdIn { get; }

    /// <summary>
    /// Only include tasks which belong to one of the passed and comma-separated 
    /// tenant ids.
    /// </summary>
    public List<string>? TenantIdIn { get; }

    /// <summary>
    /// Only include tasks which belong to no tenant. Value may only be <c>true</c>, 
    /// as <c>false</c> is the default behavior.
    /// </summary>
    public bool? WithoutTenantId { get; }

    /// <summary>
    /// Restrict to tasks that the given user is assigned to.
    /// </summary>
    public string? Assignee { get; }

    /// <summary>
    /// Restrict to tasks that the user described by the given expression is assigned to. See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a> 
    /// for more information on available functions.
    /// </summary>
    public string? AssigneeExpression { get; }

    /// <summary>
    /// Restrict to tasks that have an assignee that has the parameter 
    /// value as a substring.
    /// </summary>
    public string? AssigneeLike { get; }

    /// <summary>
    /// Restrict to tasks that have an assignee that has the parameter value described by the 
    /// given expression as a substring. See the 
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a> 
    /// for more information on available functions.
    /// </summary>
    public string? AssigneeLikeExpression { get; }

    /// <summary>
    /// Only include tasks which are assigned to one of the passed and comma-separated user ids.
    /// </summary>
    public List<string>? AssigneeIn { get; }

    /// <summary>
    /// Only include tasks which are not assigned to one of the passed and comma-separated user ids.
    /// </summary>
    public List<string>? AssigneeNotIn { get; }

    /// <summary>
    /// Restrict to tasks that the given user owns.
    /// </summary>
    public string? Owner { get; }

    /// <summary>
    /// Restrict to tasks that the user described by the given expression owns. See the 
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a> 
    /// for more information on available functions.
    /// </summary>
    public string? OwnerExpression { get; }

    /// <summary>
    /// Only include tasks that are offered to the given group.
    /// </summary>
    public string? CandidateGroup { get; }

    /// <summary>
    /// Only include tasks that are offered to groups that have the parameter value as a substring.
    /// </summary>
    public string? CandidateGroupLike { get; }

    /// <summary>
    /// Only include tasks that are offered to the group described by the given expression. 
    /// See the 
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a> 
    /// for more information on available functions.
    /// </summary>
    public string? CandidateGroupExpression { get; }

    /// <summary>
    /// Only include tasks that are offered to the given user or to one of his groups.
    /// </summary>
    public string? CandidateUser { get; }

    /// <summary>
    /// Only include tasks that are offered to the user described by the given expression. 
    /// See the 
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a> 
    /// for more information on available functions.
    /// </summary>
    public string? CandidateUserExpression { get; }

    /// <summary>
    /// Also include tasks that are assigned to users in candidate queries. Default is to only 
    /// include tasks that are not assigned to any user if you query by candidate user or
    /// group(s).
    /// </summary>
    public bool? IncludeAssignedTasks { get; }

    /// <summary>
    /// Only include tasks that the given user is involved in. A user is involved in a task if 
    /// an identity link exists between task and user (e.g., the user is the assignee).
    /// </summary>
    public string? InvolvedUser { get; }

    /// <summary>
    /// Only include tasks that the user described by the given expression is involved in.
    /// A user is involved in a task if an identity link exists between task and user
    /// (e.g., the user is the assignee). See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? InvolvedUserExpression { get; }

    /// <summary>
    /// If set to <c>true</c>, restricts the query to all tasks that are assigned.
    /// </summary>
    public bool? Assigned { get; }

    /// <summary>
    /// If set to <c>true</c>, restricts the query to all tasks that are unassigned.
    /// </summary>
    public bool? Unassigned { get; }

    /// <summary>
    /// Restrict to tasks that have the given key.
    /// </summary>
    public string? TaskDefinitionKey { get; }

    /// <summary>
    /// Restrict to tasks that have one of the given keys. The keys need to be in a comma-separated list.
    /// </summary>
    public List<string>? TaskDefinitionKeyIn { get; }

    /// <summary>
    /// Restrict to tasks that have a key that has the parameter value as a substring.
    /// </summary>
    public string? TaskDefinitionKeyLike { get; }

    /// <summary>
    /// Restrict to tasks that have the given name.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Restrict to tasks that do not have the given name.
    /// </summary>
    public string? NameNotEqual { get; }

    /// <summary>
    /// Restrict to tasks that have a name with the given parameter value as substring.
    /// </summary>
    public string? NameLike { get; }

    /// <summary>
    /// Restrict to tasks that do not have a name with the given parameter
    /// value as substring.
    /// </summary>
    public string? NameNotLike { get; }

    /// <summary>
    /// Restrict to tasks that have the given description.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Restrict to tasks that have a description that has the parameter
    /// value as a substring.
    /// </summary>
    public string? DescriptionLike { get; }

    /// <summary>
    /// Restrict to tasks that have the given priority.
    /// </summary>
    public int? Priority { get; }

    /// <summary>
    /// Restrict to tasks that have a lower or equal priority.
    /// </summary>
    public int? MaxPriority { get; }

    /// <summary>
    /// Restrict to tasks that have a higher or equal priority.
    /// </summary>
    public int? MinPriority { get; }

    /// <summary>
    /// Restrict to tasks that are due on the given date.
    /// </summary>
    public DateTime? DueDate { get; }

    /// <summary>
    /// Restrict to tasks that are due on the date described by the given expression. See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? DueDateExpression { get; }

    /// <summary>
    /// Restrict to tasks that are due after the given date.
    /// </summary>
    public DateTime? DueAfter { get; }

    /// <summary>
    /// Restrict to tasks that are due after the date described by the given expression.
    /// See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? DueAfterExpression { get; }

    /// <summary>
    /// Restrict to tasks that are due before the given date.
    /// </summary>
    public DateTime? DueBefore { get; }

    /// <summary>
    /// Restrict to tasks that are due before the date described by the given expression.
    /// See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? DueBeforeExpression { get; }

    /// <summary>
    /// Only include tasks which have no due date. Value may only be <c>true</c>, 
    /// as <c>false</c> is the default behavior.
    /// </summary>
    public bool? WithoutDueDate { get; }

    /// <summary>
    /// Restrict to tasks that have a followUp date on the given date.
    /// </summary>
    public DateTime? FollowUpDate { get; }

    /// <summary>
    /// Restrict to tasks that have a followUp date on the date described by the given
    /// expression. See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? FollowUpDateExpression { get; }

    /// <summary>
    /// Restrict to tasks that have a followUp date after the given date.
    /// </summary>
    public DateTime? FollowUpAfter { get; }

    /// <summary>
    /// Restrict to tasks that have a followUp date after the date described by the given
    /// expression. See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? FollowUpAfterExpression { get; }

    /// <summary>
    /// Restrict to tasks that have a followUp date before the given date.
    /// </summary>
    public DateTime? FollowUpBefore { get; }

    /// <summary>
    /// Restrict to tasks that have a followUp date before the date described by the given
    /// expression. See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? FollowUpBeforeExpression { get; }

    /// <summary>
    /// Restrict to tasks that have no followUp date or a followUp date before the given date.
    /// The typical use case is to query all <c>active</c> tasks for a user for a given date.
    /// </summary>
    public DateTime? FollowUpBeforeOrNotExistent { get; }

    /// <summary>
    /// Restrict to tasks that have no followUp date or a followUp date before the date
    /// described by the given expression. See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? FollowUpBeforeOrNotExistentExpression { get; }

    /// <summary>
    /// Restrict to tasks that were created on the given date.
    /// </summary>
    public DateTime? CreatedOn { get; }

    /// <summary>
    /// Restrict to tasks that were created on the date described by the given expression.
    /// See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? CreatedOnExpression { get; }

    /// <summary>
    /// Restrict to tasks that were created after the given date.
    /// </summary>
    public DateTime? CreatedAfter { get; }

    /// <summary>
    /// Restrict to tasks that were created after the date described by the given expression.
    /// See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? CreatedAfterExpression { get; }

    /// <summary>
    /// Restrict to tasks that were created before the given date.
    /// </summary>
    public DateTime? CreatedBefore { get; }

    /// <summary>
    /// Restrict to tasks that were created before the date described by the given expression.
    /// See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? CreatedBeforeExpression { get; }

    /// <summary>
    /// Restrict to tasks that were updated after the given date. Every action that fires 
    /// a <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/delegation-code/#task-listener-event-lifecycle">task update event</a> is considered as updating the task.
    /// </summary>
    public DateTime? UpdatedAfter { get; }

    /// <summary>
    /// Restrict to tasks that were updated after the date described by the given expression. Every action that fires 
    /// a <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/delegation-code/#task-listener-event-lifecycle">task update event</a> is considered as updating the task.
    /// See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? UpdatedAfterExpression { get; }

    /// <summary>
    /// Restrict to tasks that are in the given delegation state. Valid values are
    /// <c>PENDING</c> and <c>RESOLVED</c>.
    /// </summary>
    public DelegationStateType? DelegationState { get; }

    /// <summary>
    /// Restrict to tasks that are offered to any of the given candidate groups.
    /// </summary>
    public List<string>? CandidateGroups { get; }

    /// <summary>
    /// Restrict to tasks that are offered to any of the candidate groups described by the
    /// given expression. See the
    /// <a href="https://docs.camunda.org/manual/7.22/user-guide/process-engine/expression-language/#internal-context-functions">user guide</a>
    /// for more information on available functions.
    /// </summary>
    public string? CandidateGroupsExpression { get; }

    /// <summary>
    /// Only include tasks which have a candidate group. Value may only be <c>true</c>,
    /// as <c>false</c> is the default behavior.
    /// </summary>
    public bool? WithCandidateGroups { get; }

    /// <summary>
    /// Only include tasks which have no candidate group. Value may only be <c>true</c>,
    /// as <c>false</c> is the default behavior.
    /// </summary>
    public bool? WithoutCandidateGroups { get; }

    /// <summary>
    /// Only include tasks which have a candidate user. Value may only be <c>true</c>,
    /// as <c>false</c> is the default behavior.
    /// </summary>
    public bool? WithCandidateUsers { get; }

    /// <summary>
    /// Only include tasks which have no candidate users. Value may only be <c>true</c>,
    /// as <c>false</c> is the default behavior.
    /// </summary>
    public bool? WithoutCandidateUsers { get; }

    /// <summary>
    /// Only include active tasks. Value may only be <c>true</c>, as <c>false</c>
    /// is the default behavior.
    /// </summary>
    public bool? Active { get; }

    /// <summary>
    /// Only include suspended tasks. Value may only be <c>true</c>, as
    /// <c>false</c> is the default behavior.
    /// </summary>
    public bool? Suspended { get; }

    /// <summary>
    /// A JSON array to only include tasks that have variables with certain values. The
    /// array consists of JSON objects with three properties <c>name</c>, <c>operator</c> and <c>value</c>.
    /// </summary>
    public List<VariableQueryParameter>? TaskVariables { get; }

    /// <summary>
    /// A JSON array to only include tasks that belong to a process instance with variables
    /// with certain values.
    /// </summary>
    public List<VariableQueryParameter>? ProcessVariables { get; }

    /// <summary>
    /// A JSON array to only include tasks that belong to a case instance with variables
    /// with certain values.
    /// </summary>
    public List<VariableQueryParameter>? CaseInstanceVariables { get; }

    /// <summary>
    /// Match all variable names in this query case-insensitively. If set
    /// <c>variableName</c> and <c>variablename</c> are treated as equal.
    /// </summary>
    public bool? VariableNamesIgnoreCase { get; }

    /// <summary>
    /// Match all variable values in this query case-insensitively. If set
    /// <c>variableValue</c> and <c>variablevalue</c> are treated as equal.
    /// </summary>
    public bool? VariableValuesIgnoreCase { get; }

    /// <summary>
    /// Restrict query to all tasks that are sub tasks of the given task. Takes a task id.
    /// </summary>
    public string? ParentTaskId { get; }

    /// <summary>
    /// A JSON array of nested task queries with OR semantics.
    /// </summary>
    public List<TaskQuery>? OrQueries { get; }

    /// <summary>
    /// Apply sorting of the result.
    /// </summary>
    public List<TaskQuerySorting>? Sorting { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="TaskQuery"/> record.
    /// </summary>
    private TaskQuery(
        string? taskId = null,
        List<string>? taskIdIn = null,
        string? processInstanceId = null,
        List<string>? processInstanceIdIn = null,
        string? processInstanceBusinessKey = null,
        string? processInstanceBusinessKeyExpression = null,
        List<string>? processInstanceBusinessKeyIn = null,
        string? processInstanceBusinessKeyLike = null,
        string? processInstanceBusinessKeyLikeExpression = null,
        string? processDefinitionId = null,
        string? processDefinitionKey = null,
        List<string>? processDefinitionKeyIn = null,
        string? processDefinitionName = null,
        string? processDefinitionNameLike = null,
        string? executionId = null,
        string? caseInstanceId = null,
        string? caseInstanceBusinessKey = null,
        string? caseInstanceBusinessKeyLike = null,
        string? caseDefinitionId = null,
        string? caseDefinitionKey = null,
        string? caseDefinitionName = null,
        string? caseDefinitionNameLike = null,
        string? caseExecutionId = null,
        List<string>? activityInstanceIdIn = null,
        List<string>? tenantIdIn = null,
        bool? withoutTenantId = null,
        string? assignee = null,
        string? assigneeExpression = null,
        string? assigneeLike = null,
        string? assigneeLikeExpression = null,
        List<string>? assigneeIn = null,
        List<string>? assigneeNotIn = null,
        string? owner = null,
        string? ownerExpression = null,
        string? candidateGroup = null,
        string? candidateGroupLike = null,
        string? candidateGroupExpression = null,
        string? candidateUser = null,
        string? candidateUserExpression = null,
        bool? includeAssignedTasks = null,
        string? involvedUser = null,
        string? involvedUserExpression = null,
        bool? assigned = null,
        bool? unassigned = null,
        string? taskDefinitionKey = null,
        List<string>? taskDefinitionKeyIn = null,
        string? taskDefinitionKeyLike = null,
        string? name = null,
        string? nameNotEqual = null,
        string? nameLike = null,
        string? nameNotLike = null,
        string? description = null,
        string? descriptionLike = null,
        int? priority = null,
        int? maxPriority = null,
        int? minPriority = null,
        DateTime? dueDate = null,
        string? dueDateExpression = null,
        DateTime? dueAfter = null,
        string? dueAfterExpression = null,
        DateTime? dueBefore = null,
        string? dueBeforeExpression = null,
        bool? withoutDueDate = null,
        DateTime? followUpDate = null,
        string? followUpDateExpression = null,
        DateTime? followUpAfter = null,
        string? followUpAfterExpression = null,
        DateTime? followUpBefore = null,
        string? followUpBeforeExpression = null,
        DateTime? followUpBeforeOrNotExistent = null,
        string? followUpBeforeOrNotExistentExpression = null,
        DateTime? createdOn = null,
        string? createdOnExpression = null,
        DateTime? createdAfter = null,
        string? createdAfterExpression = null,
        DateTime? createdBefore = null,
        string? createdBeforeExpression = null,
        DateTime? updatedAfter = null,
        string? updatedAfterExpression = null,
        DelegationStateType? delegationState = null,
        List<string>? candidateGroups = null,
        string? candidateGroupsExpression = null,
        bool? withCandidateGroups = null,
        bool? withoutCandidateGroups = null,
        bool? withCandidateUsers = null,
        bool? withoutCandidateUsers = null,
        bool? active = null,
        bool? suspended = null,
        List<VariableQueryParameter>? taskVariables = null,
        List<VariableQueryParameter>? processVariables = null,
        List<VariableQueryParameter>? caseInstanceVariables = null,
        bool? variableNamesIgnoreCase = null,
        bool? variableValuesIgnoreCase = null,
        string? parentTaskId = null,
        List<TaskQuery>? orQueries = null,
        List<TaskQuerySorting>? sorting = null)
    {

        

        TaskId = taskId;
        TaskIdIn = taskIdIn;
        ProcessInstanceId = processInstanceId;
        ProcessInstanceIdIn = processInstanceIdIn;
        ProcessInstanceBusinessKey = processInstanceBusinessKey;
        ProcessInstanceBusinessKeyExpression = processInstanceBusinessKeyExpression;
        ProcessInstanceBusinessKeyIn = processInstanceBusinessKeyIn;
        ProcessInstanceBusinessKeyLike = processInstanceBusinessKeyLike;
        ProcessInstanceBusinessKeyLikeExpression = processInstanceBusinessKeyLikeExpression;
        ProcessDefinitionId = processDefinitionId;
        ProcessDefinitionKey = processDefinitionKey;
        ProcessDefinitionKeyIn = processDefinitionKeyIn;
        ProcessDefinitionName = processDefinitionName;
        ProcessDefinitionNameLike = processDefinitionNameLike;
        ExecutionId = executionId;
        CaseInstanceId = caseInstanceId;
        CaseInstanceBusinessKey = caseInstanceBusinessKey;
        CaseInstanceBusinessKeyLike = caseInstanceBusinessKeyLike;
        CaseDefinitionId = caseDefinitionId;
        CaseDefinitionKey = caseDefinitionKey;
        CaseDefinitionName = caseDefinitionName;
        CaseDefinitionNameLike = caseDefinitionNameLike;
        CaseExecutionId = caseExecutionId;
        ActivityInstanceIdIn = activityInstanceIdIn;
        TenantIdIn = tenantIdIn;
        WithoutTenantId = withoutTenantId;

        // Validate boolean properties that may only be true if set
        if (withoutTenantId.HasValue)
        {
            Guard.IsTrue(withoutTenantId.Value, nameof(withoutTenantId), "Value may only be true, as false is the default behavior.");
        }

        Assignee = assignee;
        AssigneeExpression = assigneeExpression;
        AssigneeLike = assigneeLike;
        AssigneeLikeExpression = assigneeLikeExpression;
        AssigneeIn = assigneeIn;
        AssigneeNotIn = assigneeNotIn;
        Owner = owner;
        OwnerExpression = ownerExpression;
        CandidateGroup = candidateGroup;
        CandidateGroupLike = candidateGroupLike;
        CandidateGroupExpression = candidateGroupExpression;
        CandidateUser = candidateUser;
        CandidateUserExpression = candidateUserExpression;
        IncludeAssignedTasks = includeAssignedTasks;
        InvolvedUser = involvedUser;
        InvolvedUserExpression = involvedUserExpression;
        Assigned = assigned;
        Unassigned = unassigned;
        TaskDefinitionKey = taskDefinitionKey;
        TaskDefinitionKeyIn = taskDefinitionKeyIn;
        TaskDefinitionKeyLike = taskDefinitionKeyLike;
        Name = name;
        NameNotEqual = nameNotEqual;
        NameLike = nameLike;
        NameNotLike = nameNotLike;
        Description = description;
        DescriptionLike = descriptionLike;
        Priority = priority;
        MaxPriority = maxPriority;
        MinPriority = minPriority;
        DueDate = dueDate;
        DueDateExpression = dueDateExpression;
        DueAfter = dueAfter;
        DueAfterExpression = dueAfterExpression;
        DueBefore = dueBefore;
        DueBeforeExpression = dueBeforeExpression;
        WithoutDueDate = withoutDueDate;

        if (withoutDueDate.HasValue)
        {
            Guard.IsTrue(withoutDueDate.Value, nameof(withoutDueDate), "Value may only be true, as false is the default behavior.");
        }

        FollowUpDate = followUpDate;
        FollowUpDateExpression = followUpDateExpression;
        FollowUpAfter = followUpAfter;
        FollowUpAfterExpression = followUpAfterExpression;
        FollowUpBefore = followUpBefore;
        FollowUpBeforeExpression = followUpBeforeExpression;
        FollowUpBeforeOrNotExistent = followUpBeforeOrNotExistent;
        FollowUpBeforeOrNotExistentExpression = followUpBeforeOrNotExistentExpression;
        CreatedOn = createdOn;
        CreatedOnExpression = createdOnExpression;
        CreatedAfter = createdAfter;
        CreatedAfterExpression = createdAfterExpression;
        CreatedBefore = createdBefore;
        CreatedBeforeExpression = createdBeforeExpression;
        UpdatedAfter = updatedAfter;
        UpdatedAfterExpression = updatedAfterExpression;
        DelegationState = delegationState;
        CandidateGroups = candidateGroups;
        CandidateGroupsExpression = candidateGroupsExpression;
        WithCandidateGroups = withCandidateGroups;

        if (withCandidateGroups.HasValue)
        {
            Guard.IsTrue(withCandidateGroups.Value, nameof(withCandidateGroups), "Value may only be true, as false is the default behavior.");
        }

        WithoutCandidateGroups = withoutCandidateGroups;

        if (withoutCandidateGroups.HasValue)
        {
            Guard.IsTrue(withoutCandidateGroups.Value, nameof(withoutCandidateGroups), "Value may only be true, as false is the default behavior.");
        }

        WithCandidateUsers = withCandidateUsers;

        if (withCandidateUsers.HasValue)
        {
            Guard.IsTrue(withCandidateUsers.Value, nameof(withCandidateUsers), "Value may only be true, as false is the default behavior.");
        }

        WithoutCandidateUsers = withoutCandidateUsers;

        if (withoutCandidateUsers.HasValue)
        {
            Guard.IsTrue(withoutCandidateUsers.Value, nameof(withoutCandidateUsers), "Value may only be true, as false is the default behavior.");
        }

        Active = active;

        if (active.HasValue)
        {
            Guard.IsTrue(active.Value, nameof(active), "Value may only be true, as false is the default behavior.");
        }

        Suspended = suspended;

        if (suspended.HasValue)
        {
            Guard.IsTrue(suspended.Value, nameof(suspended), "Value may only be true, as false is the default behavior.");
        }

        TaskVariables = taskVariables;
        ProcessVariables = processVariables;
        CaseInstanceVariables = caseInstanceVariables;
        VariableNamesIgnoreCase = variableNamesIgnoreCase;
        VariableValuesIgnoreCase = variableValuesIgnoreCase;
        ParentTaskId = parentTaskId;
        OrQueries = orQueries;

        // Validation: Each TaskQuery in OrQueries must not contain certain properties
        if (OrQueries != null)
        {
            foreach (var query in OrQueries)
            {
                ValidateOrQuery(query);
            }
        }

        Sorting = sorting;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TaskQuery"/> record.
    /// </summary>
    public static TaskQuery Create(
        string? taskId = null,
        List<string>? taskIdIn = null,
        string? processInstanceId = null,
        List<string>? processInstanceIdIn = null,
        string? processInstanceBusinessKey = null,
        string? processInstanceBusinessKeyExpression = null,
        List<string>? processInstanceBusinessKeyIn = null,
        string? processInstanceBusinessKeyLike = null,
        string? processInstanceBusinessKeyLikeExpression = null,
        string? processDefinitionId = null,
        string? processDefinitionKey = null,
        List<string>? processDefinitionKeyIn = null,
        string? processDefinitionName = null,
        string? processDefinitionNameLike = null,
        string? executionId = null,
        string? caseInstanceId = null,
        string? caseInstanceBusinessKey = null,
        string? caseInstanceBusinessKeyLike = null,
        string? caseDefinitionId = null,
        string? caseDefinitionKey = null,
        string? caseDefinitionName = null,
        string? caseDefinitionNameLike = null,
        string? caseExecutionId = null,
        List<string>? activityInstanceIdIn = null,
        List<string>? tenantIdIn = null,
        bool? withoutTenantId = null,
        string? assignee = null,
        string? assigneeExpression = null,
        string? assigneeLike = null,
        string? assigneeLikeExpression = null,
        List<string>? assigneeIn = null,
        List<string>? assigneeNotIn = null,
        string? owner = null,
        string? ownerExpression = null,
        string? candidateGroup = null,
        string? candidateGroupLike = null,
        string? candidateGroupExpression = null,
        string? candidateUser = null,
        string? candidateUserExpression = null,
        bool? includeAssignedTasks = null,
        string? involvedUser = null,
        string? involvedUserExpression = null,
        bool? assigned = null,
        bool? unassigned = null,
        string? taskDefinitionKey = null,
        List<string>? taskDefinitionKeyIn = null,
        string? taskDefinitionKeyLike = null,
        string? name = null,
        string? nameNotEqual = null,
        string? nameLike = null,
        string? nameNotLike = null,
        string? description = null,
        string? descriptionLike = null,
        int? priority = null,
        int? maxPriority = null,
        int? minPriority = null,
        DateTime? dueDate = null,
        string? dueDateExpression = null,
        DateTime? dueAfter = null,
        string? dueAfterExpression = null,
        DateTime? dueBefore = null,
        string? dueBeforeExpression = null,
        bool? withoutDueDate = null,
        DateTime? followUpDate = null,
        string? followUpDateExpression = null,
        DateTime? followUpAfter = null,
        string? followUpAfterExpression = null,
        DateTime? followUpBefore = null,
        string? followUpBeforeExpression = null,
        DateTime? followUpBeforeOrNotExistent = null,
        string? followUpBeforeOrNotExistentExpression = null,
        DateTime? createdOn = null,
        string? createdOnExpression = null,
        DateTime? createdAfter = null,
        string? createdAfterExpression = null,
        DateTime? createdBefore = null,
        string? createdBeforeExpression = null,
        DateTime? updatedAfter = null,
        string? updatedAfterExpression = null,
        DelegationStateType? delegationState = null,
        List<string>? candidateGroups = null,
        string? candidateGroupsExpression = null,
        bool? withCandidateGroups = null,
        bool? withoutCandidateGroups = null,
        bool? withCandidateUsers = null,
        bool? withoutCandidateUsers = null,
        bool? active = null,
        bool? suspended = null,
        List<VariableQueryParameter>? taskVariables = null,
        List<VariableQueryParameter>? processVariables = null,
        List<VariableQueryParameter>? caseInstanceVariables = null,
        bool? variableNamesIgnoreCase = null,
        bool? variableValuesIgnoreCase = null,
        string? parentTaskId = null,
        List<TaskQuery>? orQueries = null,
        List<TaskQuerySorting>? sorting = null)
    {
        return new TaskQuery(
            taskId,
            taskIdIn,
            processInstanceId,
            processInstanceIdIn,
            processInstanceBusinessKey,
            processInstanceBusinessKeyExpression,
            processInstanceBusinessKeyIn,
            processInstanceBusinessKeyLike,
            processInstanceBusinessKeyLikeExpression,
            processDefinitionId,
            processDefinitionKey,
            processDefinitionKeyIn,
            processDefinitionName,
            processDefinitionNameLike,
            executionId,
            caseInstanceId,
            caseInstanceBusinessKey,
            caseInstanceBusinessKeyLike,
            caseDefinitionId,
            caseDefinitionKey,
            caseDefinitionName,
            caseDefinitionNameLike,
            caseExecutionId,
            activityInstanceIdIn,
            tenantIdIn,
            withoutTenantId,
            assignee,
            assigneeExpression,
            assigneeLike,
            assigneeLikeExpression,
            assigneeIn,
            assigneeNotIn,
            owner,
            ownerExpression,
            candidateGroup,
            candidateGroupLike,
            candidateGroupExpression,
            candidateUser,
            candidateUserExpression,
            includeAssignedTasks,
            involvedUser,
            involvedUserExpression,
            assigned,
            unassigned,
            taskDefinitionKey,
            taskDefinitionKeyIn,
            taskDefinitionKeyLike,
            name,
            nameNotEqual,
            nameLike,
            nameNotLike,
            description,
            descriptionLike,
            priority,
            maxPriority,
            minPriority,
            dueDate,
            dueDateExpression,
            dueAfter,
            dueAfterExpression,
            dueBefore,
            dueBeforeExpression,
            withoutDueDate,
            followUpDate,
            followUpDateExpression,
            followUpAfter,
            followUpAfterExpression,
            followUpBefore,
            followUpBeforeExpression,
            followUpBeforeOrNotExistent,
            followUpBeforeOrNotExistentExpression,
            createdOn,
            createdOnExpression,
            createdAfter,
            createdAfterExpression,
            createdBefore,
            createdBeforeExpression,
            updatedAfter,
            updatedAfterExpression,
            delegationState,
            candidateGroups,
            candidateGroupsExpression,
            withCandidateGroups,
            withoutCandidateGroups,
            withCandidateUsers,
            withoutCandidateUsers,
            active,
            suspended,
            taskVariables,
            processVariables,
            caseInstanceVariables,
            variableNamesIgnoreCase,
            variableValuesIgnoreCase,
            parentTaskId,
            orQueries,
            sorting);
    }

    // Validation method for OrQueries
    private void ValidateOrQuery(TaskQuery query)
    {
        var forbiddenProperties = new List<string>();

        if (query.Sorting != null && query.Sorting.Count > 0)
        {
            forbiddenProperties.Add(nameof(query.Sorting));
        }

        if (query.WithCandidateGroups.HasValue)
        {
            forbiddenProperties.Add(nameof(query.WithCandidateGroups));
        }

        if (query.WithoutCandidateGroups.HasValue)
        {
            forbiddenProperties.Add(nameof(query.WithoutCandidateGroups));
        }

        if (query.WithCandidateUsers.HasValue)
        {
            forbiddenProperties.Add(nameof(query.WithCandidateUsers));
        }

        if (query.WithoutCandidateUsers.HasValue)
        {
            forbiddenProperties.Add(nameof(query.WithoutCandidateUsers));
        }

        if (forbiddenProperties.Count > 0)
        {
            var properties = string.Join(", ", forbiddenProperties);
            throw new ArgumentException($"The following properties are not allowed in OrQueries: {properties}");
        }
    }

    /// <summary>
    /// Possible delegation states for a task.
    /// </summary>
    public enum DelegationStateType
    {
        /// <summary>
        /// Task is pending delegation.
        /// </summary>
        PENDING,

        /// <summary>
        /// Task delegation is resolved.
        /// </summary>
        RESOLVED
    }
}
