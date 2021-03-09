using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class TeamResponse
    {
        [JsonPropertyName("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonPropertyName("lineup")]
        public PlayerResponse[] Lineup
        {
            get;
            set;
        }

        public PlayerResponse[] Bench
        {
            get;
            set;
        }
    }
}