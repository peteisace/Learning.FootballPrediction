using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class TeamInfo
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
    }
}