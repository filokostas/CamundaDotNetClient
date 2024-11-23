using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace CamundaClient.Infrastructure.Utilities;

public class CamundaDateTimeUtils : ICamundaDateTimeUtils
{
    public string FormatDateTime(DateTimeOffset dateTime)
    {
		string formattedDate = dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff");
		string offset = dateTime.Offset.ToString(@"\+hhmm;\-hhmm");

		return formattedDate + offset;
	}

    public string UrlEncodeDateTime(DateTimeOffset dateTime)
    {
        string formattedDate = FormatDateTime(dateTime);

        // URL-encode the formatted date string
        return WebUtility.UrlEncode(formattedDate);
    }

	//private static string RemoveTimezoneColon(string dateTime)
	//{
	//    if (dateTime.Length > 6 && (dateTime[^6] == '+' || dateTime[^6] == '-'))
	//    {
	//        return dateTime.Remove(dateTime.Length - 3, 1);
	//    }
	//    return dateTime;
	//}
}

public interface ICamundaDateTimeUtils
{
	string FormatDateTime(DateTimeOffset dateTime);
	string UrlEncodeDateTime(DateTimeOffset dateTime);
}
