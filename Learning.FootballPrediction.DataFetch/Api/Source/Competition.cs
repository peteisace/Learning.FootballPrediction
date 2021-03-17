using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class Competition
    {                
        [JsonPropertyName("competition")]
        public CompetitionInfo CompetitionDetails
        {
            get;
            set;
        }

        [JsonPropertyName("matches")]
        public List<MatchInfo> Matches
        {
            get;
            set;
        }
    }
}