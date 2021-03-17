using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class ResultContainer
    {
        [JsonPropertyName("fullTime")]
        public ResultInfo FullTime
        {
            get;
            set;
        }

        [JsonPropertyName("halfTime")]
        public ResultInfo HalfTime
        {
            get;
            set;
        }
    }
}