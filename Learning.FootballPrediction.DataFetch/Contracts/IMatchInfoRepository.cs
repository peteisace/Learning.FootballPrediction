using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Source;

namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface IMatchInfoRepository
    {
        Task<MatchInfo[]> GetMatchSummariesAsync(int year, int matchDay);

        Task<MatchResponse> GetMatchDetailsAsync(int matchId);
    }
}