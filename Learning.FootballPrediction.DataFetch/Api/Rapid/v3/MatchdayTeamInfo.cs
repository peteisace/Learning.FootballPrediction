using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid.v3
{
    public class MatchdayTeamInfo
    {
        [JsonPropertyName("team")]
        public TeamInfo TeamInfo
        {
            get;
            set;
        }

        [JsonPropertyName("players")]
        public MatchdayPlayerInfo[] Players
        {
            get;
            set;
        }
    }
}