using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid.v3
{
    public class MatchdayPlayerInfo : IJsonOnDeserialized
    {             
        public MatchdayPlayerStatistics Statistics
        {
            get;
            private set;            
        }

        [JsonPropertyName("statistics")]
        public MatchdayPlayerStatistics[] StatisticsList
        {
            get;
            set;
        }

        [JsonPropertyName("player")]
        public PlayerInfo Player
        {
            get;
            set;
        }

        public void OnDeserialized()
        {            
            this.Statistics = this.StatisticsList[0];
            this.Statistics.PlayerId = this.Player.ID;
            this.Statistics.Name = this.Player.Name;
        }
    }
}