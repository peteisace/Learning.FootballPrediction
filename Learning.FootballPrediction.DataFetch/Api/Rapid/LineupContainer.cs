using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class LineupContainer
    {
        public TeamSquadDetails Home
        {
            get;
            set;
        }

        public TeamSquadDetails Away
        {
            get;
            set;
        }
    }
}