using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class ResultInfo
    {
        [JsonPropertyName("homeTeam")]
        public int HomeTeam
        {
            get;
            set;
        }

        [JsonPropertyName("awayTeam")]
        public int AwayTeam
        {
            get;
            set;
        }
    }
}