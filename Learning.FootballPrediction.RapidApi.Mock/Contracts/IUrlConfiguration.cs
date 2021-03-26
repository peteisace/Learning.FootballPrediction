namespace Learning.FootballPrediction.RapidApi.Mock.Contracts
{
    public interface IUrlConfiguration
    {
        string BaseUrl 
        {
            get;
        }

        string Leagues 
        {
            get;
        }

        string FixturesLeague
        {
            get;
        }

        string Fixture
        {
            get;
        }

        string Players
        {
            get;
        }
    }
}