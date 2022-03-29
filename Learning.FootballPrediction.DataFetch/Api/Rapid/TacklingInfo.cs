using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class TacklingInfo
    {
        private int? _total;
        private int? _blocks;
        private int? _interceptions;

        [JsonPropertyName("total")]
        public int? Total
        {
            get => this._total ?? 0;
            set => this._total = value ?? 0;
        }

        [JsonPropertyName("blocks")]
        public int? Blocks
        {
            get => this._blocks ?? 0;
            set => this._total = value ?? 0;
        }

        [JsonPropertyName("interceptions")]
        public int? Interceptions
        {
            get => this._interceptions ?? 0;
            set => this._interceptions = value ?? 0;
        }
    }
}