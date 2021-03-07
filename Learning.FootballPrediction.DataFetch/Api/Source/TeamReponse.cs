namespace Learning.FootballPrediction.DataFetch.Api.Source
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