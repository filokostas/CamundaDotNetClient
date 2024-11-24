namespace CamundaClient.Application.Dtos.Requests;

public record TaskQueryParameters : PaginationParameters
{
	private TaskQueryParameters(int? firstResult, int? maxResults)
		: base(firstResult, maxResults)
	{
	}

	public static TaskQueryParameters Create(int? firstResult, int? maxResults)
	{
		return new TaskQueryParameters(firstResult, maxResults);
	}
}
