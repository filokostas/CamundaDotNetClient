using CamundaClient.Core.Models.Responses;

namespace CamundaClient.Core.Exceptions;
public class CamundaApiException : Exception
{
    public int StatusCode { get; }
    public CamundaError? ErrorDetails { get; }

    public CamundaApiException(string message, int statusCode, CamundaError? errorDetails = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorDetails = errorDetails;
    }

    public override string ToString()
    {
        var details = ErrorDetails != null
            ? $", Type: {ErrorDetails.Type}, Code: {ErrorDetails.Code}, Message: {ErrorDetails.Message}"
            : string.Empty;

        return $"{base.ToString()}, StatusCode: {StatusCode}{details}";
    }
}