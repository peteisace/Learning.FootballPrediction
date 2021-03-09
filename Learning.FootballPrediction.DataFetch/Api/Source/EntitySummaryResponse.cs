using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class EntitySummaryResponse
    {
        [JsonPropertyName("id")]
        public int Id
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
    }
    
}