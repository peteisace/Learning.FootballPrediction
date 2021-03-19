using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class FixtureInfoResult
    {
        [JsonPropertyName("api")]
        public FixtureInfoContainer Api
        {
            get;
            set;
        }
    }
}