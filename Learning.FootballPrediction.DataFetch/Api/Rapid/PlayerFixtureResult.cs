using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class PlayerFixtureResult
    {
        [JsonPropertyName("api")]
        public PlayerFixtureContainer Api
        {
            get;
            set;
        }
    }
}