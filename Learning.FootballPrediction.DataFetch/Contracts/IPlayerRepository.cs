using System.Collections.Generic;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Rapid;

namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface IPlayerRepository
    {
        Task<PlayerDetailResult> GetPlayerAsync(int id, int season);

        Task<Dictionary<int, PlayerRatingInfo>> GetRatingsForFixtureAsync(int fixtureId);
    }
}