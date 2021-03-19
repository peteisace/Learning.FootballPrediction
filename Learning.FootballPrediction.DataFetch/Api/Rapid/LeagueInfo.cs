using System;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class LeagueInfo
    {
        [JsonPropertyName("league_id")]
        public int LeagueId
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

        [JsonPropertyName("season_start")]
        public DateTime SeasonStart
        {
            get;
            set;
        }

        [JsonPropertyName("season_end")]
        public DateTime SeasonEnd
        {
            get;
            set;
        }

        [JsonPropertyName("season")]
        public int Season
        {
            get;
            set;
        }
    }
}