using System;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class FixtureInfo
    {
        [JsonPropertyName("fixture_id")]
        public int FixtureId
        {
            get;
            set;
        }

        [JsonPropertyName("league_id")]
        public int LeagueId
        {
            get;
            set;
        }

        [JsonPropertyName("event_date")]
        public DateTime EventDate
        {
            get;
            set;
        }

        [JsonPropertyName("venue")]
        public string Venue
        {
            get;
            set;
        }

        [JsonPropertyName("homeTeam")]
        public TeamInfo HomeTeam
        {
            get;
            set;
        }

        [JsonPropertyName("awayTeam")]
        public TeamInfo AwayTeam
        {
            get;
            set;
        }

        [JsonPropertyName("goalsHomeTeam")]
        public int GoalsHomeTeam
        {
            get;
            set;
        }

        [JsonPropertyName("goalsAwayTeam")]
        public int GoalsAwayTeam
        {
            get;
            set;
        }
    }
}