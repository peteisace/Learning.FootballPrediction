using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class FixtureDetails
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
        public int EventDate
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

        [JsonPropertyName("home_team")]
        public TeamInfo HomeTeam
        {
            get;
            set;
        }

        [JsonPropertyName("away_team")]
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

        [JsonPropertyName("events")]
        public List<MatchEvent> Events
        {
            get;
            set;
        }

        [JsonPropertyName("lineups")]
        public Dictionary<string, TeamSquadDetails> Lineups
        {
            get;
            set;
        }
    }
}