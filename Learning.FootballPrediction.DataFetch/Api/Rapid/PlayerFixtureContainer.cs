using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class PlayerFixtureContainer
    {
        [JsonPropertyName("results")]
        public int Results
        {
            get;
            set;
        }

        [JsonPropertyName("players")]
        public List<PlayerRatingInfo> Players
        {
            get;
            set;
        }
    }
}