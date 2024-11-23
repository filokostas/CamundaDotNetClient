using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;

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
		if (reader.TokenType == JsonToken.Null)
		{
			return null;
		}

		if (reader.TokenType != JsonToken.String)
		{
			throw new JsonSerializationException($"Unexpected token parsing date. Expected String, got {reader.TokenType}.");
		}

		// Read the date string
		string dateString = (string)reader.Value!;

		try
		{
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
		catch (FormatException ex)
		{
			throw new JsonSerializationException($"Error parsing date string: {dateString}. Expected format: {CamundaDateFormat}", ex);
		}
	}

	private string RemoveTimezoneColon(string dateTime)
	{
		// Remove the colon from the timezone offset (e.g., +02:00 -> +0200)
		return Regex.Replace(dateTime, @"([+-]\d{2}):(\d{2})$", "$1$2");
	}

	private string AddTimezoneColon(string dateTime)
	{
		// Add the colon back into the timezone offset (e.g., +0200 -> +02:00)
		return Regex.Replace(dateTime, @"([+-]\d{2})(\d{2})$", "$1:$2");
	}
}
