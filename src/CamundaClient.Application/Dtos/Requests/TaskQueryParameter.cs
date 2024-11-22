using CamundaClient.Application.Utilities;

namespace CamundaClient.Application.Dtos.Requests;

public record TaskQueryParameter
{
    public int? FirstResult { get; }
    public int? MaxResults { get; }

    private TaskQueryParameter(int? firstResult, int? maxResults)
    {
        FirstResult = firstResult;
        MaxResults = maxResults;
    }

    public static TaskQueryParameter Create(int? firstResult, int? maxResults)
    {
        if ((firstResult.HasValue && !maxResults.HasValue) || (!firstResult.HasValue && maxResults.HasValue))
        {
            throw new ArgumentException("Both firstResult and maxResults must have values if either one is not null.");
        }

        return new TaskQueryParameter(firstResult, maxResults);
    }
}
