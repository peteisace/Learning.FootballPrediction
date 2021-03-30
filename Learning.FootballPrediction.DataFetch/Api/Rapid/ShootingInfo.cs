using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class ShootingInfo
    {
        [JsonPropertyName("total")]
        public int Total
        {
            get;
            set;
        }

        [JsonPropertyName("on")]
        public int OnTarget
        {
            get;
            set;
        }
    }
}