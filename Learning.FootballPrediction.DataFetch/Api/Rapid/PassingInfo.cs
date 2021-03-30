using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class PassingInfo
    {
        [JsonPropertyName("total")]
        public int Total
        {
            get;
            set;
        }

        [JsonPropertyName("key")]
        public int Key
        {
            get;
            set;
        }

        [JsonPropertyName("accuracy")]
        public int Accuracy
        {
            get;
            set;
        }
    }
}