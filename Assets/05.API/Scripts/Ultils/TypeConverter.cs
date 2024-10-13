using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System;

public class TypeConverter<T> : StringEnumConverter where T : Enum
{
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            string value = reader.Value.ToString();
            foreach (var member in Enum.GetValues(typeof(T)))
            {
                var enumMember = (T)member;
                var enumMemberAttribute = enumMember.GetAttributeOfType<EnumMemberAttribute>();
                if (enumMemberAttribute != null && enumMemberAttribute.Value == value)
                {
                    return enumMember;
                }
            }
        }

        return base.ReadJson(reader, objectType, existingValue, serializer);
    }
}

public static class EnumExtensions
{
    public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
        return attributes.Length > 0 ? (T)attributes[0] : null;
    }
}