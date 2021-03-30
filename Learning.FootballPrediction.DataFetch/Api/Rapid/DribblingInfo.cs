using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid   
{
    public class DribblingInfo
    {
        [JsonPropertyName("attempts")]
        public int Attempts
        {
            get;
            set;
        }

        [JsonPropertyName("success")]
        public int Success
        {
            get;
            set;
        }

        [JsonPropertyName("past")]
        public int Past
        {
            get;
            set;
        }
    }
}