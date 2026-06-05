using System;
using Newtonsoft.Json;

namespace Grimoire.Tools;

public class IntStringConverter : JsonConverter
{
	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		writer.WriteValue(((string)value == "undefined") ? ((object)0) : value);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		return reader.Value.ToString() == "1";
	}

	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(string);
	}
}
