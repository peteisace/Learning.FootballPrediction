using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid.v3
{    
    public class ErrorInfo
    {
       [JsonPropertyName("error")]
        public string Error 
        {
            get;
            set;
        }        
    }
}