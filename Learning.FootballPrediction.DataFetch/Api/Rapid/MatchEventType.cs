using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    [JsonConverter(typeof(EnumMemberConverterFactory))]
    public enum MatchEventType
    {
        [EnumMember(Value = "Goal")]
        Goal = 1,

        [EnumMember(Value = "Card")]
        Card = 2,

        [EnumMember(Value = "subst")]
        Susbtitution = 3
    }

    public class EnumMemberConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(typeToConvert == typeof(T))
            {
                // Grab the string value.
                var value = reader.GetString();

                // Now go through the enumeration.
                var enumerationValues = Enum.GetValues<T>();

                foreach(var val in enumerationValues)
                {
                    var memberInfo = typeof(T).GetMember(val.ToString());
                    var attrib = memberInfo.Length == 1 ? memberInfo[0].GetCustomAttributes(false) : new EnumMemberAttribute[0];

                    // see that its value
                    var attribValue = attrib.Length == 1 && attrib[0] is EnumMemberAttribute ? (attrib[0] as EnumMemberAttribute).Value : null;
                    if(attribValue != null && attribValue == value) 
                    {
                        return (T)val;
                    }
               }
            }

            throw new InvalidOperationException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var memberData = typeof(T).GetMember(value.ToString());
            if(memberData.Length == 1)
            {
                var attributes = memberData[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if(attributes.Length == 1)
                {
                    var valueToWrite = (attributes[0] as EnumMemberAttribute).Value;
                    writer.WriteStringValue(valueToWrite);
                }
            }
        }
    }

    public class EnumMemberConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if(typeToConvert == typeof(MatchEventType))
            {
                return new EnumMemberConverter<MatchEventType>();
            }

            return null;
        }
    }
}