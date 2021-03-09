using System;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class MatchResponse
    {

        [JsonPropertyName("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonPropertyName("utcDate")]
        public DateTime UtcDate
        {
            get;
            set;
        }

        [JsonPropertyName("attendance")]
        public int Attendance
        {
            get;
            set;
        }

        [JsonPropertyName("matchday")]
        public int MatchDay
        {
            get;
            set;
        }       

        [JsonPropertyName("homeTeam")]
        public TeamResponse HomeTeam
        {
            get;
            set;
        }

        [JsonPropertyName("awayTeam")]
        public TeamResponse AwayTeam
        {
            get;
            set;
        }

        [JsonPropertyName("goals")]
        public GoalResponse[] Goals
        {
            get;
            set;
        }

        public SubstitutionResponse[] Substitutions
        {
            get;
            set;
        }
    }
}