using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CamundaClient.Infrastructure.Configuration;

public class JsonSerializerSettingsConfig
{
    public JsonSerializerSettings Settings { get; set; }

    public JsonSerializerSettingsConfig()
    {
        Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };
    }
}
