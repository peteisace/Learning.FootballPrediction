using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace Learning.FootballPrediction.Api.Models   
{
    //[JsonConverter(typeof(EnumStringConverter<MeasurementType>))]
    public enum MeasurementType : byte
    {
        [EnumMember(Value = "cm")]
        cm = 1,

        [EnumMember(Value = "kg")]
        kg = 2
    }
}