

namespace Learning.FootballPrediction.DataFetch
{    
    public class MatchConfig : IMatchConfiguration
    {
        public MatchConfig()
        {
        }

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

        ///

        public string Ratings
        {
            get;
            set;
        }

        ///

        public string PlayerFixture
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

        public string DestinationBase
        {
            get;
            set;
        }

        public void SetBaseUrl(string baseUrl, string apiKey, string apiHost)
        {
         
            this.BaseUrl = baseUrl;
            this.ApiKey = apiKey;            
            this.ApiHost = apiHost;
        }

        public void SetDestination(string destinationBase)
        {
            this.DestinationBase = destinationBase;
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}