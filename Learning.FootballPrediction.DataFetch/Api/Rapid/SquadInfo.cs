using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class SquadInfo
    {
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

        [JsonPropertyName("number")]
        public int Number
        {
            get;
            set;
        }        

        [JsonPropertyName("pos")]
        public string Position
        {
            get;
            set;
        }
    }
}