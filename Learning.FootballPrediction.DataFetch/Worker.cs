using System;
using System.Threading;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Learning.FootballPrediction.DataFetch.Api.Source;
using IO.Swagger.Api;
using System.Text.Json;

namespace Learning.FootballPrediction.DataFetch
{
    public class Worker : IHostedService
    {
        private IMatchInfoRepository _matchRepository;
        private IPlayerRepository _playerRepository;
        private IRunConfiguration _runParameters;
        private IMatchConfiguration _mConfig;

        private ILeagueRepository _leagueRepository;

        public Worker(ILeagueRepository leagueRepository, IMatchInfoRepository matchRepository, IPlayerRepository playerRepository, IRunConfiguration runParameters, IMatchConfiguration mConfig)
        {
            this._matchRepository = matchRepository;
            this._playerRepository = playerRepository;
            this._runParameters = runParameters;
            this._mConfig = mConfig;
            this._leagueRepository = leagueRepository;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Get the list of leagues we need to deal with.
            var listOfLeagues = await this._leagueRepository.GetEnglishLeaguesAsync();

            foreach(var league in listOfLeagues.Api.Leagues)
            {
                // See that we are not dealing in a year that we should not.
                if(this._runParameters.StartingSeason > league.Season)
                {
                    continue;
                }

                // Now for each league we need to get the set of fixtures associated with that league.
                var competitionDetails = await this._leagueRepository.GetFixturesForSeason(league.LeagueId);

                // Using this - get the match details for each fixture.
                foreach(var fixture in competitionDetails.Api.Fixtures)
                {
                    // Grab the fixture details
                    var matchDetails = await this._matchRepository.GetMatchDetailsAsync(fixture.FixtureId);

                    // And turn it into a match request.
                    var converter = new MatchConverter(this._playerRepository, league.Season);
                    var mr = await converter.ToMatch(matchDetails.Api.Fixtures[0]);

                    // Serialize it to a string.  
                    //var serialized = JsonSerializer.Serialize<IO.Swagger.Model.MatchRequest>(mr);
                    //Console.WriteLine(serialized);
                
                    // Go execute it.
                    MatchApi mApi = new MatchApi(this._mConfig.DestinationBase);
                    //var newtonsoft = mApi.Configuration.ApiClient.Serialize(mr);
                    //Console.WriteLine(newtonsoft);

                    //mApi.Configuration.AddDefaultHeader("Content-Type", "application/json");
                    var result = await mApi.MatchSavePostAsync(mr);
                }
            }

            /*

            // Now we go round our competition.
            for(var i = 1; i <= this._runParameters.MatchDays; i++)
            {
                // Using the match repository, grab it.
                var matchesForDay = await this._matchRepository.GetMatchSummariesAsync(this._runParameters.StartingSeason, i);

                // Now we need to suck up the players.
                foreach(var m in matchesForDay.Matches)
                {
                    // we need to get the match details.
                    var matchContainer = await this._matchRepository.GetMatchDetailsAsync(m.ID);
                    var matchDetails = matchContainer.Match;

                    // Now queue up the players.
                    var allPlayers = matchDetails.HomeTeam.Lineup
                        .Union(matchDetails.HomeTeam.Bench)
                        .Union(matchDetails.AwayTeam.Lineup)
                        .Union(matchDetails.AwayTeam.Bench);

                    // Using all of the players - create hash.
                    var playerHash = new Dictionary<int, PlayerResponse>();
                    foreach(var p in allPlayers)
                    {
                        playerHash.Add(p.Id, p);
                    }

                    // Now convert it
                    var mc = new MatchConverter(this._playerRepository);
                    var request = await mc.ToMatch(matchDetails, playerHash);

                    var api = new MatchApi(this._mConfig.DestinationBase);
                    var response = await api.MatchSavePostAsync(body: request);
                }
            }

            */
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
        }
    }
}