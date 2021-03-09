namespace Learning.FootballPrediction.DataFetch.Api.Destination
{
    public class TeamResponse
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public PlayerResponse[] Lineup
        {
            get;
            set;
        }

        public PlayerResponse[] Bench
        {
            get;
            set;
        }
    }
}