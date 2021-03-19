using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class MatchEvent
    {
        [JsonPropertyName("elapsed")]
        public int Elapsed
        {
            get;
            set;
        }

        [JsonPropertyName("team_id")]
        public int TeamId
        {
            get;
            set;
        }

        [JsonPropertyName("player_id")]
        public int PlayerId
        {
            get;
            set;
        }

        [JsonPropertyName("player")]
        public string Player
        {
            get;
            set;
        }

        [JsonPropertyName("type")]
        public MatchEventType Type
        {
            get;
            set;
        }

        [JsonPropertyName("detail")]
        public string Detail
        {
            get;
            set;
        }
    }
}