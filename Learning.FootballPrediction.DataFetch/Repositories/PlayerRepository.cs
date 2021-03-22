using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Rapid;
using Learning.FootballPrediction.DataFetch.Contracts;
using RestSharp;

namespace Learning.FootballPrediction.DataFetch.Repositories
{
    public class PlayerRepository : HttpRepositoryBase, IPlayerRepository
    {
        public PlayerRepository(IMatchConfiguration configuration) : base(configuration)
        {
        }

        public async Task<PlayerDetailResult> GetPlayerAsync(int id, int season)
        {
            var seasonString = string.Concat(season, "-", season + 1);
            var response = await this.FetchOverHttp<PlayerDetailResult>(this.Configuration.Player, Method.GET, id, season);
            return response;
        }
    }
}