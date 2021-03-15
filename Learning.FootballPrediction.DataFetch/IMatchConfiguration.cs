namespace Learning.FootballPrediction.DataFetch
{
    public interface IMatchConfiguration
    {
        string Competition 
        {
            get;
        }

        string Match
        {
            get;
        }

        string Player
        {
            get;
        }

        string BaseUrl
        {
            get;
        }

        string ApiKey
        {
            get;
        }
    }
}