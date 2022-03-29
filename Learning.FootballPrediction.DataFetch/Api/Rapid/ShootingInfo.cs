using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class ShootingInfo
    {
        private int? _total;
        private int? _onTarget;

        [JsonPropertyName("total")]
        public int? Total
        {
            get => this._total ?? 0;
            set => this._total = value ?? 0;            
        }

        [JsonPropertyName("on")]
        public int? OnTarget
        {
            get => this._onTarget ?? 0;
            set => this._onTarget = value ?? 0;
        }
    }
}