using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid.v3
{
    public class GameInfo
    {
        private int? _minutes;
        private int? _number;
        
        [JsonPropertyName("minutes")]
        public int? Minutes 
        {
            get => this._minutes ?? 0;
            set => this._minutes = value ?? 0;
        }

        [JsonPropertyName("number")]
        public int? Number
        {
            get => this._number ?? 0;
            set => this._number = value ?? 0;
        }

        [JsonPropertyName("position")]
        public string Position
        {
            get;
            set;
        }

        [JsonPropertyName("rating")]
        public string Rating
        {
            get;
            set;
        }

        [JsonPropertyName("captain")]
        public bool IsCaptain
        {
            get;
            set;
        }

        [JsonPropertyName("substitute")]
        public bool IsSubstitute
        {
            get;
            set;
        }
    }
}