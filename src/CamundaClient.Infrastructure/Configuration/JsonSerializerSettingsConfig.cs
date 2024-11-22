using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CamundaClient.Infrastructure.Configuration;

public class JsonSerializerSettingsConfig
{
    public JsonSerializerSettings Settings { get; set; }

    public JsonSerializerSettingsConfig(IEnumerable<JsonConverter> converters)
    {
		Settings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			NullValueHandling = NullValueHandling.Ignore,
			Formatting = Formatting.Indented,
			DateTimeZoneHandling = DateTimeZoneHandling.Local,
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateParseHandling = DateParseHandling.None // Prevent automatic date parsing
		};

		foreach (var converter in converters)
		{
			Settings.Converters.Add(converter);
		}
	}
}
