

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace PalladiumDwh.DWapi
{
	public class CaseSensitiveDeserializer : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			JObject target = new JObject();

			foreach (JProperty property in JToken.Load(reader).Children())
			{
				if (objectType.GetProperty(property.Name) != null)
				{
					target.Add(property);
					//property = GetProperty(property.Name, StringComparison.Ordinal);
				}
			}

			return target.ToObject(objectType);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JObject o = (JObject)JToken.FromObject(value);
			o.WriteTo(writer);
		}
	}

}