﻿using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CamundaClient.Infrastructure.Interfaces;

namespace CamundaClient.Infrastructure.Utilities;
public class NewtonsoftJsonSerializer : IJsonSerializer
{
	private static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
	{
		ContractResolver = new CamelCasePropertyNamesContractResolver(),
		NullValueHandling = NullValueHandling.Ignore,
		Formatting = Formatting.Indented
	};

	public string Serialize<T>(T obj)
	{
		return JsonConvert.SerializeObject(obj, DefaultSettings);
	}

	public T Deserialize<T>(string json)
	{
		return JsonConvert.DeserializeObject<T>(json, DefaultSettings);
	}
}