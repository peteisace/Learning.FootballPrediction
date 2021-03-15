namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface IRunConfiguration
    {
        int MatchDays 
        {
            get;
        }

        int StartingSeason
        {
            get;
        }
    }
}