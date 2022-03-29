using System.Collections.Generic;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Rapid;
using Learning.FootballPrediction.DataFetch.Api.Rapid.v3;

namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface IPlayerRepository
    {
        Task<PlayerDetailResult> GetPlayerAsync(int id, int season);

        Task<Dictionary<int, PlayerRatingInfo>> GetRatingsForFixtureAsync(int fixtureId);

        Task<Dictionary<int, MatchdayPlayerStatistics>> GetPlayerRatingsAsync(int fixtureId);
    }
}