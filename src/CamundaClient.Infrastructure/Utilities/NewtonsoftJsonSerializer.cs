using CamundaClient.Infrastructure.Exceptions;
using CamundaClient.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CamundaClient.Infrastructure.Utilities;
public class NewtonsoftJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerSettings _settings;

    public NewtonsoftJsonSerializer(JsonSerializerSettings settings)
    {
        _settings = settings;
    }

    public string Serialize<T>(T obj)
	{
		return JsonConvert.SerializeObject(obj, _settings);
	}

	public T Deserialize<T>(string json)
	{
		try
		{
			return JsonConvert.DeserializeObject<T>(json, _settings)!;
		}
		catch (JsonException ex)
		{
			throw new DeserializationException($"Failed to deserialize JSON: {json}", ex);
		}
	}
}
