namespace CamundaClient.Application.Dtos.Requests;

public record PaginationParameters
{
	public int? FirstResult { get; }
	public int? MaxResults { get; }

	protected PaginationParameters(int? firstResult, int? maxResults)
	{
		if ((firstResult.HasValue && !maxResults.HasValue) || (!firstResult.HasValue && maxResults.HasValue))
		{
			throw new ArgumentException("Both firstResult and maxResults must have values if either one is not null.");
		}

		FirstResult = firstResult;
		MaxResults = maxResults;
	}
}
