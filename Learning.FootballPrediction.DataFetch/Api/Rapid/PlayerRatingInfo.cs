using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class PlayerRatingInfo
    {
        [JsonPropertyName("player_id")]
        public int PlayerId
        {
            get;
            set;
        }

        [JsonPropertyName("player_name")]
        public string Name
        {
            get;
            set;
        }

        [JsonPropertyName("number")]
        public int Number
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

        [JsonPropertyName("minutes_played")]
        public int MinutesPlayed
        {
            get;
            set;
        }

        [JsonPropertyName("shots")]
        public ShootingInfo Shots
        {
            get;
            set;
        }

        [JsonPropertyName("passes")]
        public PassingInfo Passes
        {
            get;
            set;
        }

        [JsonPropertyName("tackles")]
        public TacklingInfo Tackles
        {
            get;
            set;
        }

        [JsonPropertyName("dribbles")]
        public DribblingInfo Dribbles
        {
            get;
            set;
        }

        [JsonPropertyName("fouls")]
        public FoulingInfo Fouls
        {
            get;
            set;
        }
    }
}