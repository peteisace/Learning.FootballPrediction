using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class GoalResponse
    {
        [JsonPropertyName("minute")]
        public int Minute
        {
            get;
            set;
        }

        [JsonPropertyName("type")]
        public string Type
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

        [JsonPropertyName("scorer")]
        public EntitySummaryResponse Scorer
        {
            get;
            set;
        }
    }
}