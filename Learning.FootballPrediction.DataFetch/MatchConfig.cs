namespace Learning.FootballPrediction.DataFetch
{
    public class MatchConfig : IMatchConfiguration
    {
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
            set;
        }

        public string BaseUrl
        {
            get;
            set;
        }

        public void SetBaseUrl(string baseUrl, string apiKey)
        {
            /*
            this.Competition = string.Concat(baseUrl, this.Competition);
            this.Match = string.Concat(baseUrl, this.Match);
            this.Player = string.Concat(baseUrl, this.Player);
            */
            this.BaseUrl = baseUrl;
            this.ApiKey = apiKey;
        }
    }
}