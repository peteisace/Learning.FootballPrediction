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
            System.Console.WriteLine("I am instantiated for type {0}", typeof(T).FullName);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            System.Console.WriteLine("In CanConvert typeToConvert=={0}", typeToConvert.FullName);
            return true;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // grab the value.
            var value = reader.GetString();
            System.Console.WriteLine("Value = {0}", value);
            T result = default(T);

            if(value == null || !Enum.TryParse<T>(value, false, out result))
            {
                System.Console.WriteLine("Shit. Stuck in exception, value == null = {0}, Enum.TryParse<T> == {1}", value == null, Enum.TryParse<T>(value, false, out result));
                throw new SerializationException($"Cannot convert value ${value ?? "NULL"}");
            }

            System.Console.WriteLine("Returning result of {0}", result);
            return result;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            System.Console.WriteLine("In writer, value = {0}", value.ToString());
            var stringRep = value.ToString();
            JsonSerializer.Serialize(writer, stringRep);
        }
    }
}