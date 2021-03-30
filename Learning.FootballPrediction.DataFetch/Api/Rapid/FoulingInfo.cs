using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class FoulingInfo
    {
        [JsonPropertyName("drawn")]
        public int Drawn
        {
            get;
            set;
        }

        [JsonPropertyName("committed")]
        public int Committed
        {
            get;
            set;
        }
    }
}