using Learning.FootballPrediction.DataFetch.Contracts;

namespace Learning.FootballPrediction.DataFetch
{
    public class RunConfiguration : IRunConfiguration
    {
        public RunConfiguration(int noOfMatchdays, int startYear)
        {
            MatchDays = noOfMatchdays;
            StartingSeason = startYear;
        }
        
        public int MatchDays { get; }
        public int StartingSeason { get; }
    }
}