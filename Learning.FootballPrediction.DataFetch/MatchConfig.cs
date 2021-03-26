namespace Learning.FootballPrediction.DataFetch
{
    public class MatchConfig : IMatchConfiguration
    {
        public string League
        {
            get;    
            set;
        }
        
        public string Competition
        {
            get;
            set;
        }

        public string Match
        {
            get;
            set;
        }

        public string Player
        {
            get;
            set;
        }

        public string ApiKey
        {
            get;
            private set;
        }

        public string BaseUrl
        {
            get;
            private set;
        }

        public string ApiHost
        {
            get;
            private set;
        }

        public void SetBaseUrl(string baseUrl, string apiKey)
        {
         
            this.BaseUrl = baseUrl;
            this.ApiKey = apiKey;            

        }

        public void SetDestination(string destinationBase)
        {
            this.ApiHost = destinationBase;
        }
        
    }
}