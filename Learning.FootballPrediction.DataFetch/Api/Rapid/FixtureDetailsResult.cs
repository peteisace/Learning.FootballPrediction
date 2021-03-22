using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class FixtureDetailsResult
    {
        [JsonPropertyName("api")]
        public FixtureDetailsContainer Api
        {
            get;
            set;
        }
    }
}