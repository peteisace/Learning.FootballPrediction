using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class TeamInfo
    {
        [JsonPropertyName("team_id")]
        public int TeamId
        {
            get;
            set;
        }

        [JsonPropertyName("team_name")]
        public string Name
        {
            get;
            set;
        }
    }
}