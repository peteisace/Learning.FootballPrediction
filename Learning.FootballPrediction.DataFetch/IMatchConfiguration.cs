namespace Learning.FootballPrediction.DataFetch
{
    public interface IMatchConfiguration
    {
        string League
        {
            get;
        }
        
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
        string ApiHost 
        { 
            get; 
        }

        string DestinationBase
        {
            get;
        }
        string Ratings { get; set; }
    }
}