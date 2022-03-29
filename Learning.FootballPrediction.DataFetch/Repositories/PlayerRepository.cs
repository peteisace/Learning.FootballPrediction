using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Rapid;
using Learning.FootballPrediction.DataFetch.Api.Rapid.v3;
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
            var response = await this.FetchOverHttp<PlayerDetailResult>(this.Configuration.Player, Method.GET, id, seasonString);
            return response;
        }

        public async Task<Dictionary<int, MatchdayPlayerStatistics>> GetPlayerRatingsAsync(int fixtureId)
        {
            var response = await this.FetchOverHttp<FixtureResultInfo>(this.Configuration.PlayerFixture, Method.GET, fixtureId);
            Dictionary<int, MatchdayPlayerStatistics> ratings = new Dictionary<int, MatchdayPlayerStatistics>();            
            foreach(var team in response.Response)
            {
                foreach(var player in team.Players)
                {
                    if(player.Statistics.PlayerId.HasValue) 
                    {                        
                        ratings.Add(player.Statistics.PlayerId.Value, player.Statistics);
                    }
                }
            }

            return ratings;
        }

        public async Task<Dictionary<int, PlayerRatingInfo>> GetRatingsForFixtureAsync(int fixtureId)
        {
            var response = await this.FetchOverHttp<PlayerFixtureResult>(this.Configuration.Ratings, Method.GET, fixtureId);
            Dictionary<int, PlayerRatingInfo> ratings = new Dictionary<int, PlayerRatingInfo>();
            // Go through the response.
            foreach(var player in response.Api.Players)
            {
                if(player.PlayerId.HasValue)
                {
                    ratings.Add(player.PlayerId.Value, player);
                }
            }

            return ratings;
        }
    }
}