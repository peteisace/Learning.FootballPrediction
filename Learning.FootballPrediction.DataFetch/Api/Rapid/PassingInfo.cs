using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class PassingInfo : IJsonOnDeserialized
    {
        private int? _total;
        private int? _key;
        private int? _accuracy;                
        private string _accuracyString;

        [JsonPropertyName("total")]
        public int? Total
        {
            get => this._total ?? 0;
            set => this._total = value ?? 0;
        }

        [JsonPropertyName("key")]
        public int? Key
        {
            get => this._key ?? 0;
            set => this._key = value ?? 0;
        }
        
        public int? Accuracy
        {
            get => this._accuracy ?? 0;
            set => this._accuracy = value ?? 0;
        }

        [JsonPropertyName("accuracy")]
        public string AccuracyPercentage
        {
            get => this._accuracyString;
            set => this._accuracyString = value;
        }

        void IJsonOnDeserialized.OnDeserialized()
        {
            if(this._accuracyString != null && this._accuracyString.Contains("%"))
            {
                string removed = this._accuracyString.Remove(this._accuracyString.IndexOf("%"));
                int result;
                if(int.TryParse(removed, out result))
                {
                    this.Accuracy = result;
                }
            }                        
            else
            {
                int result;
                if(int.TryParse(this._accuracyString, out result))
                {
                    this.Accuracy = result;
                }
            }
        }
    }
}