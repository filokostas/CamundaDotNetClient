namespace CamundaClient.Application.Dtos.Responses;

/// <summary>
/// Represents the Camunda form reference.
/// </summary>
public record CamundaFormRef
{
	/// <summary>
	/// The key of the Camunda Form.
	/// </summary>
	public string? Key { get; init; }

	/// <summary>
	/// The binding of the Camunda Form. Can be `latest`, `deployment` or `version`.
	/// </summary>
	public string? Binding { get; init; }

	/// <summary>
	/// The specific version of a Camunda Form. This property is only set if `binding` is `version`.
	/// </summary>
	public int? Version { get; init; }
}
