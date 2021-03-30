using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace Learning.FootballPrediction.Api.Models
{
    //[JsonConverter(typeof(EnumStringConverter<EventType>))]
    public enum EventType : byte
    {
        [EnumMember(Value = "GoalScored")]
        GoalScored = 1,

        [EnumMember(Value = "PenaltySuccess")]
        PenaltySuccess = 2,

        [EnumMember(Value = "PenaltyMiss")]
        PenaltyMiss = 3,

        [EnumMember(Value = "YellowCard")]
        YellowCard = 4,

        [EnumMember(Value = "RedCard")]
        RedCard = 5,

        [EnumMember(Value = "SubOn")]
        SubOn = 6,

        [EnumMember(Value = "SubOff")]
        SubOff = 7,

        [EnumMember(Value = "Assist")]
        Assist = 8
    }
}