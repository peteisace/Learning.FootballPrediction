using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid   
{
    public class DribblingInfo
    {
        private int? _attempts;
        private int? _success;
        private int? _past;

        [JsonPropertyName("attempts")]
        public int? Attempts
        {
            get => this._attempts ?? 0;
            set => this._attempts = value ?? 0;
        }

        [JsonPropertyName("success")]
        public int? Success
        {
            get => this._success ?? 0;
            set => this._success = value ?? 0;
        }

        [JsonPropertyName("past")]
        public int? Past
        {
            get => this._past ?? 0;
            set => this._past = value ?? 0;
        }
    }
}