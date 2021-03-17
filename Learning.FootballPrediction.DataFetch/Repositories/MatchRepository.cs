using Learning.FootballPrediction.DataFetch.Contracts;
using RestSharp;
using Learning.FootballPrediction.DataFetch.Api.Source;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using System;
using System.Linq;

namespace Learning.FootballPrediction.DataFetch.Repositories
{
    public class MatchRepository : IMatchInfoRepository
    {
        private IMatchConfiguration _configuration;

        public MatchRepository(IMatchConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<Competition> GetMatchSummariesAsync(int year, int matchDay)
        {
            var client = new RestClient(this._configuration.BaseUrl);
            // Get request
            IRestRequest request = new RestRequest(string.Format(this._configuration.Competition, year, matchDay), Method.GET);
            request = request.AddHeader("X-Auth-Token", this._configuration.ApiKey);
            var response = await client.GetAsync<Competition>(request);

            return response;
        }

        public async Task<MatchContainer> GetMatchDetailsAsync(int matchId)
        {
            var client = new RestClient(this._configuration.BaseUrl);
            // Get request
            IRestRequest request = new RestRequest(string.Format(this._configuration.Match, matchId), Method.GET);
            request = request.AddHeader("X-Auth-Token", this._configuration.ApiKey);
            var response = await client.GetAsync<MatchContainer>(request);

            return response;
        }
    }
}
