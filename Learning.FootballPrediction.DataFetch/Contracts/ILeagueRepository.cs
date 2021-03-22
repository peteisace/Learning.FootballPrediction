using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Rapid;

namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface ILeagueRepository
    {
        Task<LeagueResult> GetEnglishLeaguesAsync();

        Task<FixtureInfoResult> GetFixturesForSeason(int leagueId);
    }
}