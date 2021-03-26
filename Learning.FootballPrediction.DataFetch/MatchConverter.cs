using IO.Swagger.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Contracts;
using Learning.FootballPrediction.DataFetch.Api.Rapid;
using System.Linq;
using MatchEvent = Learning.FootballPrediction.DataFetch.Api.Rapid.MatchEvent;
using System;
using System.Globalization;
using System.Text;

namespace Learning.FootballPrediction.DataFetch
{
    public class MatchConverter
    {
        private IPlayerRepository _players;

        private int _season;

        public MatchConverter(IPlayerRepository players, int season)
        {
            this._players = players;
            PlayerCache.Instance.RegisterHandler(this.PlayerDetailRequest);
            this._season = season;
        }

        public async Task<MatchRequest> ToMatch(FixtureDetails fixture)
        {
             // Create the basic container.
            var m = new MatchRequest();
            m.Played = fixture.EventDate;

            // Set up the team information.
            var home = fixture.Lineups[fixture.HomeTeam.Name];
            var away = fixture.Lineups[fixture.AwayTeam.Name];
            
            // Create club, players with events.
            m.Home = await this.CreateClub(fixture.HomeTeam, home, fixture.Events.Where(p => p.TeamId == fixture.HomeTeam.TeamId).ToList(), fixture.LeagueId);
            m.Away = await this.CreateClub(fixture.AwayTeam, away, fixture.Events.Where(p => p.TeamId == fixture.AwayTeam.TeamId).ToList(), fixture.LeagueId);

            return m;
        }

        private async Task<ClubRequest> CreateClub(TeamInfo team, TeamSquadDetails squad, List<MatchEvent> events, int leagueId)
        {
            List<PlayerRequest> players = new List<PlayerRequest>();
            Dictionary<int, PlayerRequest> requestPlayers = new Dictionary<int, PlayerRequest>();

            foreach(var p in squad.StartXi.Union(squad.Substitutes))
            {
                // Get from playercache.
                var detailsResponse = await PlayerCache.Instance.Lookup(p, leagueId);

                if(detailsResponse.Response == null)
                {
                    continue;
                }

                var pDetails = detailsResponse.Response;
                var formatDate = pDetails.BirthDate.Split('/');
                var sb = new StringBuilder();

                foreach(var part in formatDate)
                {
                    var formatted = part;
                    if(part.Length < 2)
                    {
                        formatted = part.PadLeft(2, '0');
                    }

                    sb.Append(formatted);
                    sb.Append("/");
                }

                sb = sb.Remove(sb.Length - 1, 1);

                // Create player request
                var player = new PlayerRequest() { 
                    Name = pDetails.PlayerName,
                    DateOfBirth = DateTime.ParseExact(sb.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Position = pDetails.Position,
                    ActiveInEvents = new List<MatchEventRequest>()
                };

                // Temporarily store lookup.
                requestPlayers.Add(p.PlayerId, player);
            }

            // Go through the match events.
            var matchEvents = EventConverter.Convert(events.ToArray());

            // Match 'em up.
            foreach(var kvp in matchEvents)
            {
                var pRequest = requestPlayers[kvp.Key];
                pRequest.ActiveInEvents = kvp.Value;
            }

            // Create the club
            var c = new ClubRequest();
            c.Name = team.Name;
            c.Players = requestPlayers.Values.ToList();

            return c;
        }

        private async Task<PlayerDetailResult> PlayerDetailRequest(int playerId)
        {
            var response = await this._players.GetPlayerAsync(playerId, this._season);
            return response;
        }
    }    
}