using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace CamundaClient.Core.Utilities;
public static class JsonHelper
{
    private static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented
    };

    public static string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, DefaultSettings);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, DefaultSettings);
    }
}
