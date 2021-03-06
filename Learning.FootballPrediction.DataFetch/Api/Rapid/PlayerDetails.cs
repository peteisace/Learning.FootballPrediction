using System;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class PlayerDetails
    {
        [JsonPropertyName("player_id")]
        public int PlayerId
        {
            get;
            set;
        }

        [JsonPropertyName("player_name")]
        public string PlayerName
        {
            get;
            set;
        }

        [JsonPropertyName("firstname")]
        public string FirstName
        {
            get;
            set;
        }

        [JsonPropertyName("lastname")]
        public string LastName
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

        [JsonPropertyName("birth_date")]
        public string BirthDate
        {
            get;
            set;
        }

        [JsonPropertyName("birth_place")]
        public string BirthPlace
        {
            get;
            set;
        }

        [JsonPropertyName("birth_country")]
        public string BirthCountry
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

        [JsonPropertyName("height")]
        public string Height
        {
            get;
            set;
        }

        [JsonPropertyName("weight")]
        public string Weight
        {
            get;
            set;
        }

        [JsonPropertyName("league_id")]
        public int? LeagueId
        {
            get;
            set;
        }
    }
}