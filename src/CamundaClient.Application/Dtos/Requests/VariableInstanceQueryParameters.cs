namespace CamundaClient.Application.Dtos.Requests;

public record VariableInstanceQueryParameters : PaginationParameters
{
	public bool DeserializeValues { get; }
	private VariableInstanceQueryParameters(int? firstResult, int? maxResults, bool desirializeValues = true)
		: base(firstResult, maxResults)
	{
		DeserializeValues = desirializeValues;
	}
	public static VariableInstanceQueryParameters Create(int? firstResult, int? maxResults, bool desirializeValues = true)
	{
		return new VariableInstanceQueryParameters(firstResult, maxResults, desirializeValues);
	}
}
