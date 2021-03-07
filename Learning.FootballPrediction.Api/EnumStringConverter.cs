using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

namespace Learning.FootballPrediction.Api
{
    public class EnumStringConverter<T> : JsonConverter<T> where T : struct
    {
        public EnumStringConverter()
        {            
            
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // grab the value.
            var value = reader.GetString();
            T result = default(T);

            if(value == null || !Enum.TryParse<T>(value, false, out result))
            {
                throw new SerializationException($"Cannot convert value ${value ?? "NULL"}");
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var stringRep = value.ToString();
            JsonSerializer.Serialize(writer, stringRep);
        }
    }
}