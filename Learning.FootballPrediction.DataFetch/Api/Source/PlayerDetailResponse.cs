using System;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class PlayerDetailResponse
    {
        [JsonPropertyName("id")]
        public int ID
        {
            get;
            set;
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonPropertyName("firstName")]
        public string FirstName
        {
            get;
            set;
        }

        [JsonPropertyName("lastName")]
        public string LastName
        {
            get;
            set;
        }

        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth
        {
            get;
            set;
        }

        [JsonPropertyName("nationality")]
        public string Nationality
        {
            get;
            set;
        }

        [JsonPropertyName("position")]
        public string Position
        {
            get;
            set;
        }
    }
}