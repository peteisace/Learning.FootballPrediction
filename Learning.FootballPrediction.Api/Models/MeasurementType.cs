using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.Api.Models   
{
    [JsonConverter(typeof(EnumStringConverter<MeasurementType>))]
    public enum MeasurementType : byte
    {
        cm = 1,

        kg = 2
    }
}