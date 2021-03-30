using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class TacklingInfo
    {
        [JsonPropertyName("total")]
        public int Total
        {
            get;
            set;
        }

        [JsonPropertyName("blocks")]
        public int Blocks
        {
            get;
            set;
        }

        [JsonPropertyName("interceptions")]
        public int Interceptions
        {
            get;
            set;
        }
    }
}