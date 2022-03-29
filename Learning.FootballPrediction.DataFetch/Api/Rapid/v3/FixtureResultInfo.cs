using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid.v3
{
    public class FixtureResultInfo
    {
        [JsonPropertyName("response")]
        public MatchdayTeamInfo[] Response
        {
            get;
            set;
        }

        [JsonPropertyName("errors")]
        public ErrorInfo[] Errors
        {
            get;
            set;
        }
    }
}