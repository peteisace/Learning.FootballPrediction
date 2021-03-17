using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class MatchInfo
    {
        public MatchInfo()
        {
        }
        
        [JsonPropertyName("id")]
        public int ID
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

        [JsonPropertyName("score")]
        public ResultContainer ScoreContainer
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
    }
}