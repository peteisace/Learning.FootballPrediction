using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class MatchContainer
    {
        [JsonPropertyName("match")]
        public MatchResponse Match
        {
            get;
            set;
        }
    }
}