using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Source;
using Learning.FootballPrediction.DataFetch.Contracts;
using RestSharp;

namespace Learning.FootballPrediction.DataFetch.Repositories
{
    public class MatchInfoRepository : IMatchInfoRepository
    {
        private IMatchConfiguration _configuration;

        public MatchInfoRepository(IMatchConfiguration configuration)
        {
            this._configuration = configuration;    
        }

        public async Task<MatchInfo[]> GetMatchSummariesAsync(int year, int matchDay)
        {
            var client = new RestClient(this._configuration.BaseUrl);
            // Get request
            var request = new RestRequest(this._configuration.Competition, Method.GET);
            request.AddHeader("X-Auth-Token", this._configuration.ApiKey);
            return await client.GetAsync<MatchInfo[]>(request);
        }

        public async Task<MatchResponse> GetMatchDetailsAsync(int matchId)
        {
            var client = new RestClient(this._configuration.BaseUrl);
            // Get request
            var request = new RestRequest(this._configuration.Match, Method.GET);
            request.AddHeader("X-Auth-Token", this._configuration.ApiKey);
            return await client.GetAsync<MatchResponse>(request);
        }
    }
}