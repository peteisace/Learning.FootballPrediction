using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid.v3
{
    public class MatchdayPlayerStatistics
    {                
        public int? PlayerId
        {
            get;
            internal set;                        
        }
        
        public string Name
        {
            get;
            internal set;
        }
        
        public int? Number
        {
            get => this.Game?.Number ?? 0;
        }

        public string Rating
        {
            get => this.Game?.Rating ?? string.Empty;
        }
        
        public int? MinutesPlayed
        {
            get {                
                return this.Game?.Minutes;
            }
        }

        [JsonPropertyName("games")]
        public GameInfo Game
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