using Newtonsoft.Json;
using System.Globalization;

namespace CamundaClient.Infrastructure.Utilities;

public class CamundaDateTimeConverter : JsonConverter<DateTime?>
{
    private const string CamundaDateFormat = "yyyy-MM-dd'T'HH:mm:ss.fffK";

	public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
	{
		if ( value is DateTime dateTime)
		{
			// Format the DateTime into the Camunda date format
			string formattedDate = dateTime.ToString(CamundaDateFormat, CultureInfo.InvariantCulture);

			// Remove the colon from the timezone offset
			formattedDate = RemoveTimezoneColon(formattedDate);

			writer.WriteValue(formattedDate);
		}
		else
		{
			// Handle null DateTime?
			writer.WriteNull();
		}
	}

	public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.String)
		{
			// Read the date string
			string dateString = (string)reader.Value!;

			// Add the colon back into the timezone offset for parsing
			dateString = AddTimezoneColon(dateString);

			// Parse the date string into a DateTime
			var dateTime = DateTime.ParseExact(
				dateString,
				CamundaDateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.AdjustToUniversal);

			return dateTime;
		}

		throw new JsonSerializationException($"Unexpected token parsing date. Expected String, got {reader.TokenType}.");
	}

	//public override bool CanConvert(Type objectType)
	//{
	//    return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
	//}

	//public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
	//{
	//    if (value is DateTime dateTime)
	//    {
	//        // Format the DateTime into the Camunda date format
	//        string formattedDate = dateTime.ToString(CamundaDateFormat, CultureInfo.InvariantCulture);

	//        // Remove the colon from the timezone offset
	//        formattedDate = RemoveTimezoneColon(formattedDate);

	//        writer.WriteValue(formattedDate);
	//    }
	//    else
	//    {
	//        // Handle null DateTime?
	//        writer.WriteNull();
	//    }
	//}

	//public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
	//{
	//    if (reader.TokenType == JsonToken.String)
	//    {
	//        // Read the date string
	//        string dateString = (string)reader.Value!;

	//        // Add the colon back into the timezone offset for parsing
	//        dateString = AddTimezoneColon(dateString);

	//        // Parse the date string into a DateTime
	//        var dateTime = DateTime.ParseExact(
	//            dateString,
	//            CamundaDateFormat,
	//            CultureInfo.InvariantCulture,
	//            DateTimeStyles.AdjustToUniversal);

	//        return dateTime;
	//    }
	//    else if (reader.TokenType == JsonToken.Null && objectType == typeof(DateTime?))
	//    {
	//        // Handle null value for DateTime?
	//        return null;
	//    }

	//    throw new JsonSerializationException($"Unexpected token parsing date. Expected String or Null, got {reader.TokenType}.");
	//}

	private static string RemoveTimezoneColon(string dateTime)
    {
        // Remove the colon from the timezone offset (e.g., +02:00 -> +0200)
        if (dateTime.Length > 6 && (dateTime[^6] == '+' || dateTime[^6] == '-'))
        {
            return dateTime.Remove(dateTime.Length - 3, 1);
        }
        return dateTime;
    }

    private static string AddTimezoneColon(string dateTime)
    {
        // Add the colon back into the timezone offset (e.g., +0200 -> +02:00)
        if (dateTime.Length > 5 && (dateTime[^5] == '+' || dateTime[^5] == '-'))
        {
            return dateTime.Insert(dateTime.Length - 2, ":");
        }
        return dateTime;
    }
}
