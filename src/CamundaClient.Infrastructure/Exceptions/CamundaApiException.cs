using CamundaClient.Application.Dtos.Exceptions;

namespace CamundaClient.Infrastructure.Exceptions;
public class CamundaApiException : Exception
{
    public int StatusCode { get; }
    public CamundaException? ErrorDetails { get; }

    public CamundaApiException(string message, int statusCode, CamundaException? errorDetails = null)
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