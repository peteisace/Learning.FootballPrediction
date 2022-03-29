using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class FoulingInfo
    {
        private int? _drawn;
        private int? _committed;

        [JsonPropertyName("drawn")]
        public int? Drawn
        {
            get => this._drawn ?? 0;
            set => this._drawn = value ?? 0;
        }

        [JsonPropertyName("committed")]
        public int? Committed
        {
            get => this._committed ?? 0;
            set => this._committed = value ?? 0;
        }
    }
}