using Learning.FootballPrediction.DataFetch.Contracts;
using RestSharp;
using Learning.FootballPrediction.DataFetch.Api.Source;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using System;
using System.Linq;
using Learning.FootballPrediction.DataFetch.Api.Rapid;

namespace Learning.FootballPrediction.DataFetch.Repositories
{
    public class MatchRepository : HttpRepositoryBase, IMatchInfoRepository
    {
        public MatchRepository(IMatchConfiguration configuration) : base(configuration)
        {
        }

        public async Task<FixtureDetailsResult> GetMatchDetailsAsync(int matchId)
        {
            var fixtureDetails = await this.FetchOverHttp<FixtureDetailsResult>(this.Configuration.Match, parameters: matchId);
            return fixtureDetails;
        }
    }
}
