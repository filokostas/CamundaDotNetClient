using CamundaClient.Application.Dtos.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CamundaClient.Infrastructure.Utilities;

public class CamundaVariableConverter : JsonConverter<CamundaVariable>
{
	public override CamundaVariable? ReadJson(JsonReader reader, Type objectType, CamundaVariable? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var jsonObject = JObject.Load(reader);
		var value = jsonObject["value"]?.ToObject<object>(serializer);
		var type = jsonObject["type"]?.ToObject<string>(serializer);
		var valueInfo = jsonObject["valueInfo"]?.ToObject<Dictionary<string, object>>(serializer);
		var local = jsonObject["local"]?.ToObject<bool?>(serializer);

		if (value == null)
		{
			throw new JsonSerializationException("Value cannot be null.");
		}

		return CamundaVariable.Create(value, type, valueInfo, local);
	}

	public override void WriteJson(JsonWriter writer, CamundaVariable? value, JsonSerializer serializer)
	{
		var jsonObject = new JObject
		{
			{ "value", JToken.FromObject(value!.Value!, serializer) }
		};

		if (value.Type != null)
		{
			jsonObject.Add("type", JToken.FromObject(value.Type, serializer));
		}

		if (value.ValueInfo != null)
		{
			jsonObject.Add("valueInfo", JToken.FromObject(value.ValueInfo, serializer));
		}

		if (value.Local != null)
		{
			jsonObject.Add("local", JToken.FromObject(value.Local, serializer));
		}

		jsonObject.WriteTo(writer);
	}
}
