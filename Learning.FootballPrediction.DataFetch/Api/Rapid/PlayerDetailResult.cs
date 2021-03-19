using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class PlayerDetailResult
    {
        [JsonPropertyName("api")]
        public PlayerDetailContainer Api
        {
            get;
            set;
        }
    }
}