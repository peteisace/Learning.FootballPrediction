using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class LeagueContainer
    {
        [JsonPropertyName("results")]
        public int Results
        {
            get;
            set;
        }

        [JsonPropertyName("leagues")]
        public List<LeagueInfo> Leagues
        {
            get;
            set;
        }
    }
}