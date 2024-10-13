using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class DefaultStructConverter<T> : JsonConverter where T : struct
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(T);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            var defaultValueProperty = typeof(T).GetProperty("Default", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            if (defaultValueProperty != null)
            {
                return defaultValueProperty.GetValue(null);
            }
            else
            {
                return Activator.CreateInstance(typeof(T));
            }
        }

        var token = JToken.Load(reader);
        return token.ToObject<T>();
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
