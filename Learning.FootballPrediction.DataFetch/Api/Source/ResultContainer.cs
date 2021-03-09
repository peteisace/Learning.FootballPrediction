using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class ResultContainer
    {
        [JsonPropertyName("fullTime")]
        public ResultInfo Result
        {
            get;
            set;
        }
    }
}