using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class CompetitionInfo
    {
        [JsonPropertyName("id")]
        public int ID
        {
            get;
            set;
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonPropertyName("code")]
        public string Code
        {
            get;
            set;
        }

        [JsonPropertyName("plan")]
        public string Plan
        {
            get;
            set;
        }

    }
}