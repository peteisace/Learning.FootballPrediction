using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class ResultInfo
    {
        [JsonPropertyName("homeTeam")]
        public int Home
        {
            get;
            set;
        }

        [JsonPropertyName("awayTeam")]
        public int Away
        {
            get;
            set;
        }
    }
}