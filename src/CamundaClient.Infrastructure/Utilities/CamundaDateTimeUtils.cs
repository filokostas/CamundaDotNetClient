using System.Globalization;
using System.Net;

namespace CamundaClient.Infrastructure.Utilities;

public static class CamundaDateTimeUtils
{
    private const string CamundaDateFormat = "yyyy-MM-dd'T'HH:mm:ss.fffK";

    public static string FormatDateTime(DateTime dateTime)
    {
        string formattedDate = dateTime.ToString(CamundaDateFormat, CultureInfo.InvariantCulture);

        // Remove the colon from the timezone offset
        formattedDate = RemoveTimezoneColon(formattedDate);

        return formattedDate;
    }

    public static string UrlEncodeDateTime(DateTime dateTime)
    {
        string formattedDate = FormatDateTime(dateTime);

        // URL-encode the formatted date string
        return WebUtility.UrlEncode(formattedDate);
    }

    private static string RemoveTimezoneColon(string dateTime)
    {
        if (dateTime.Length > 6 && (dateTime[^6] == '+' || dateTime[^6] == '-'))
        {
            return dateTime.Remove(dateTime.Length - 3, 1);
        }
        return dateTime;
    }
}
