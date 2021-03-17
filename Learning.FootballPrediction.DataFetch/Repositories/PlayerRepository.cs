using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Source;
using Learning.FootballPrediction.DataFetch.Contracts;
using RestSharp;

namespace Learning.FootballPrediction.DataFetch.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private IMatchConfiguration _configuration;

        public PlayerRepository(IMatchConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<PlayerDetailResponse> GetPlayerAsync(int id)
        {
            RestClient client = new RestClient(this._configuration.BaseUrl);
            var request = new RestRequest(string.Format(this._configuration.Player, id), Method.GET);
            request.AddHeader("X-Auth-Token", this._configuration.ApiKey);
            // Get the result
            var p = await client.GetAsync<PlayerDetailResponse>(request);
            return p;
        }
    }
}