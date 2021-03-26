using Learning.FootballPrediction.RapidApi.Mock.Contracts;

namespace Learning.FootballPrediction.RapidApi.Mock
{
    public class UrlConfiguration : IUrlConfiguration
    {
        public string BaseUrl 
        {
            get;
        }

        public string Leagues 
        {
            get;
        }

        public string FixturesLeague
        {
            get;
        }

        public string Fixture
        {
            get;
        }

        public string Players 
        {
            get;
        }
    }
}