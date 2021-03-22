using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class FixtureDetailsContainer
    {
        [JsonPropertyName("results")]
        public int Results
        {
            get;
            set;
        }

        [JsonPropertyName("fixtures")]
        public List<FixtureDetails> Fixtures
        {
            get;
            set;
        }
    }
}