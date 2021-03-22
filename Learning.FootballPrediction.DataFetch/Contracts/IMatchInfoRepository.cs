using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Rapid;

namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface IMatchInfoRepository
    {
        Task<FixtureDetailsResult> GetMatchDetailsAsync(int matchId);
    }
}