using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Source;

namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface IMatchInfoRepository
    {
        Task<Competition> GetMatchSummariesAsync(int year, int matchDay);

        Task<MatchContainer> GetMatchDetailsAsync(int matchId);
    }
}