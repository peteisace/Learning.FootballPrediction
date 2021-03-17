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

namespace Learning.FootballPrediction.DataFetch
{
    public class Worker : IHostedService
    {
        private IMatchInfoRepository _matchRepository;
        private IPlayerRepository _playerRepository;
        private IRunConfiguration _runParameters;

        public Worker(IMatchInfoRepository matchRepository, IPlayerRepository playerRepository, IRunConfiguration runParameters)
        {
            this._matchRepository = matchRepository;
            this._playerRepository = playerRepository;
            this._runParameters = runParameters;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Now we go round our competition.
            for(var i = 1; i <= this._runParameters.MatchDays; i++)
            {
                // Using the match repository, grab it.
                var matchesForDay = await this._matchRepository.GetMatchSummariesAsync(this._runParameters.StartingSeason, i);

                // Now we need to suck up the players.
                foreach(var m in matchesForDay.Matches)
                {
                    // we need to get the match details.
                    var matchDetails = await this._matchRepository.GetMatchDetailsAsync(m.ID);

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
                }
            }
            // We will get year
            await Task.Yield();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
        }
    }
}