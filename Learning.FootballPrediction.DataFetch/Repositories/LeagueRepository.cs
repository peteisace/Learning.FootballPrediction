using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Rapid;
using Learning.FootballPrediction.DataFetch.Contracts;

namespace Learning.FootballPrediction.DataFetch.Repositories
{
    public class LeagueRepository : HttpRepositoryBase, ILeagueRepository
    {
        public LeagueRepository(IMatchConfiguration configuration) : base(configuration)
        {

        }
        public async Task<LeagueResult> GetEnglishLeaguesAsync()
        {
            // Grab over http.
            var leagueResult = await this.FetchOverHttp<LeagueResult>(this.Configuration.League);
            return leagueResult;
        }

        public async Task<FixtureInfoResult> GetFixturesForSeason(int leagueId)
        {
            var fixtures = await this.FetchOverHttp<FixtureInfoResult>(this.Configuration.Competition, parameters: leagueId);
            return fixtures;
        }
    }
}