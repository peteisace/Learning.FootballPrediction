using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class SubstitutionResponse
    {
        [JsonPropertyName("minute")]
        public int Minute
        {
            get;
            set;
        }

        [JsonPropertyName("team")]
        public EntitySummaryResponse Team
        {
            get;
            set;
        }

        [JsonPropertyName("playerIn")]
        public EntitySummaryResponse PlayerIn
        {
            get;
            set;
        }

        [JsonPropertyName("playerOut")]
        public EntitySummaryResponse PlayerOut
        {
            get;
            set;
        }
    }
}