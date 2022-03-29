using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid.v3
{
    public class PlayerInfo
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