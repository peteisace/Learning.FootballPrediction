using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MatchEventType
    {
        [EnumMember(Value = "Goal")]
        Goal = 1,

        [EnumMember(Value = "Card")]
        Card = 2,

        [EnumMember(Value = "subst")]
        Susbtitution = 3
    }
}