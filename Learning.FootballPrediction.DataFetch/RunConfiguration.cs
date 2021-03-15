using Learning.FootballPrediction.DataFetch.Contracts;

namespace Learning.FootballPrediction.DataFetch
{
    public class RunConfiguration : IRunConfiguration
    {
        public RunConfiguration(string[] commandLine)
        {
            
        }

        public int MatchDays
        {
            get;
            set;
        }

        public int StartingSeason
        {
            get;
            set;
        }
    }
}