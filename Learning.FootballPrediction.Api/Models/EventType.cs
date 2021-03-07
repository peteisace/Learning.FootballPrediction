using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.Api.Models
{
    [JsonConverter(typeof(EnumStringConverter<EventType>))]
    public enum EventType : byte
    {
        GoalScored = 1,

        PenaltySuccess = 2,

        PenaltyMiss = 3,

        YellowCard = 4,

        RedCard = 5,

        SubOn = 6,

        SubOff = 7
    }
}