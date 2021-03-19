using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class LeagueResult
    {
        [JsonPropertyName("api")]
        public LeagueContainer Api
        {
            get;
            set;
        }
    }
}