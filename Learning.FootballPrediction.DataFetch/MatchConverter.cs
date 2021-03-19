using IO.Swagger.Model;
using System.Linq;
using Learning.FootballPrediction.DataFetch.Api.Source;
using System.Collections.Generic;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Contracts;
using System;

namespace Learning.FootballPrediction.DataFetch
{
    public class MatchConverter
    {
        private IPlayerRepository _players;
        public MatchConverter(IPlayerRepository players)
        {
            this._players = players;
            PlayerCache.Instance.RegisterHandler(this.PlayerDetailRequest);
        }

        public async Task<MatchRequest> ToMatch(MatchResponse matchDetails, Dictionary<int, PlayerResponse> playerHash)
        {
             // Create the container.
            var m = new MatchRequest();
            m.Played = matchDetails.UtcDate;

            var clubRequest = await this.CreateClub(matchDetails.HomeTeam, matchDetails.Goals);  
            m.Home = clubRequest;
            clubRequest = await this.CreateClub(matchDetails.AwayTeam, matchDetails.Goals);
            m.Away = clubRequest;

            return m;
        }

        private async Task<ClubRequest> CreateClub(TeamResponse team, List<GoalResponse> goals)
        {
            Dictionary<int, PlayerRequest> playersForTeam = new Dictionary<int, PlayerRequest>();
            foreach(var p in team.Lineup.Union(team.Bench))
            {
                var lookup = await PlayerCache.Instance.Lookup(p);
                var detailed = lookup.Response;

                Console.WriteLine($"Lookup resonse is {lookup.Found} for player {p.Id}");

                PlayerRequest request = new PlayerRequest(
                    detailed.Name,
                    detailed.DateOfBirth,
                    p.Position,
                    new List<MatchEventRequest>()
                );

                // Add it to our list.  
                playersForTeam.Add(p.Id, request);

                if(string.IsNullOrEmpty(request.Name))
                {
                    throw new ArgumentNullException($"Player found with no name! ID is {p.Id}");
                }

                if(!lookup.Found)
                {
                    Console.WriteLine($"Fetched player {lookup.Response.Name}");
                }

                // Sleep so we don't overload.
                await Task.Delay(3000); 
            }

            // Now go through the goals.
            foreach(var g in goals.Where(p => p.Team.Id == team.Id))
            {
                var p = playersForTeam[g.Scorer.Id];
                // List should be fine. 

                // Add the event
                p.ActiveInEvents.Add(new MatchEventRequest(
                    g.Minute,
                    EventType.GoalScored
                ));
            }

            // Create the team.
            var cr = new ClubRequest(
                team.Name,
                playersForTeam.Values.ToList()
            );

            return cr;
        }

        private async Task<PlayerDetailResponse> PlayerDetailRequest(int playerId)
        {
            var response = await this._players.GetPlayerAsync(playerId);
            return response;
        }
    }    
}