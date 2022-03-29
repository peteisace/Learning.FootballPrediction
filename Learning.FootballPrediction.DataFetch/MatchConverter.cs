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
using Learning.FootballPrediction.DataFetch.Api.Rapid.v3;

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
            Console.WriteLine("Fixture for {0} vs. {1} (id: {2})",
                fixture.HomeTeam.Name,
                fixture.AwayTeam.Name,
                fixture.FixtureId);

             // Create the basic container.
            var m = new MatchRequest();
            m.Played = fixture.EventDate;

            // Set up the team information.
            var home = fixture.Lineups[fixture.HomeTeam.Name];
            var away = fixture.Lineups[fixture.AwayTeam.Name];

            Dictionary<int, PlayerRequest> playersFound = new Dictionary<int, PlayerRequest>();
            var ratings = await this._players.GetPlayerRatingsAsync(fixture.FixtureId);
            
            // Create club, players with events.
            m.Home = await this.CreateClub(
                fixture.HomeTeam, 
                home, 
                fixture.LeagueId, 
                playersFound,
                ratings);

            m.Away = await this.CreateClub(
                fixture.AwayTeam, 
                away, 
                fixture.LeagueId, 
                playersFound,
                ratings);

            // Go through the match events.
            var matchEvents = EventConverter.Convert(fixture.Events.ToArray());

            // Match 'em up.
            foreach(var kvp in matchEvents)
            {
                if(playersFound.ContainsKey(kvp.Key)) 
                {
                    var pRequest = playersFound[kvp.Key];
                    pRequest.ActiveInEvents = kvp.Value;
                }
                else
                {
                    Console.WriteLine($"COULD NOT FIND PLAYER WITH API_ID {kvp.Key} for the match {m.Home.Name} vs. {m.Away.Name} ({m.Played})");
                }
            }

            return m;
        }

        private static int RemoveMeasurement(string measurement)
        {
            if(string.IsNullOrEmpty(measurement))
            {
                return 0;
            }

            var parts = measurement.Split(' ');
            if(parts.Length == 2)
            {
                return int.Parse(parts[0]);
            }

            if(parts.Length == 1)
            {
                return int.Parse(measurement);
            }

            throw new ArgumentNullException();
        }
       
        private async Task<ClubRequest> CreateClub(TeamInfo team, TeamSquadDetails squad, int leagueId, Dictionary<int, PlayerRequest> playersFound, Dictionary<int, MatchdayPlayerStatistics> ratings)
        {
            List<PlayerRequest> players = new List<PlayerRequest>();
            Dictionary<int, PlayerRequest> requestPlayers = new Dictionary<int, PlayerRequest>();
            double ratingAmount = 0;
            
            foreach(var p in squad.StartXi)
            {                
                // Get from playercache.
                var detailsResponse = await PlayerCache.Instance.Lookup(p, team.TeamId, leagueId);
                var pRatings = ratings.GetValueOrDefault(p.PlayerId) ?? null;

                if(detailsResponse != null && detailsResponse.Response != null)
                {
                     var pDetails = detailsResponse.Response;
                     var validRating = double.TryParse(pRatings?.Rating ?? string.Empty, out ratingAmount);

                    // Create player request
                    var player = new PlayerRequest() { 
                        Name = pDetails.LastName == null ? pDetails.PlayerName : string.Concat(pDetails.FirstName, " ", pDetails.LastName),
                        DateOfBirth = ConvertIsoDatetime(pDetails.BirthDate),
                        Position = pDetails.Position,
                        ActiveInEvents = new List<MatchEventRequest>(),
                        Height = RemoveMeasurement(pDetails.Height),
                        HeightType = MeasurementType.Cm,
                        Weight = RemoveMeasurement(pDetails.Weight),
                        WeightType = MeasurementType.Kg,                        
                        Rating = new MatchRatings(
                            pRatings?.MinutesPlayed ?? 0,
                            validRating ? ratingAmount : 0,
                            pRatings?.Passes?.Total ?? 0,
                            pRatings?.Passes?.Key ?? 0,
                            pRatings?.Passes?.Accuracy ?? 0,
                            pRatings?.Shots?.Total ?? 0,
                            pRatings?.Shots?.OnTarget ?? 0,
                            pRatings?.Tackles?.Total ?? 0,
                            pRatings?.Tackles?.Blocks ?? 0,
                            pRatings?.Tackles?.Interceptions ?? 0,
                            pRatings?.Dribbles?.Attempts ?? 0,
                            pRatings?.Dribbles?.Success ?? 0,
                            pRatings?.Dribbles?.Past ?? 0,
                            pRatings?.Fouls?.Committed ?? 0,
                            pRatings?.Fouls?.Drawn ?? 0
                        )
                    };

                    // Temporarily store lookup.
                    requestPlayers.Add(p.PlayerId, player);
                    playersFound.Add(p.PlayerId, player);
                }
                else
                {                    
                    var validRating = double.TryParse(pRatings?.Rating ?? string.Empty, out ratingAmount);

                    // Try to create from TeamInfo
                    var player = new PlayerRequest() {
                        Name = p.Player,
                        DateOfBirth = new DateTime(1900, 1, 1),
                        Position = GetPositionFromShortcut(p.Position),
                        ActiveInEvents = new List<MatchEventRequest>(),
                        Height = null,
                        HeightType = MeasurementType.Cm,
                        Weight = null,
                        WeightType = MeasurementType.Kg,
                        Rating = pRatings == null ? new MatchRatings() : new MatchRatings(
                            pRatings?.MinutesPlayed ?? 0,
                            validRating ? ratingAmount : 0,
                            pRatings?.Passes?.Total ?? 0,
                            pRatings?.Passes?.Key ?? 0,
                            pRatings?.Passes?.Accuracy ?? 0,
                            pRatings?.Shots?.Total ?? 0,
                            pRatings?.Shots?.OnTarget ?? 0,
                            pRatings?.Tackles?.Total ?? 0,
                            pRatings?.Tackles?.Blocks ?? 0,
                            pRatings?.Tackles?.Interceptions ?? 0,
                            pRatings?.Dribbles?.Attempts ?? 0,
                            pRatings?.Dribbles?.Success ?? 0,
                            pRatings?.Dribbles?.Past ?? 0,
                            pRatings?.Fouls?.Committed ?? 0,
                            pRatings?.Fouls?.Drawn ?? 0
                        )
                    };

                    // Store it
                    requestPlayers.Add(p.PlayerId, player);
                    playersFound.Add(p.PlayerId, player);
                }
            }

            // Do substitutes.
            foreach(var p in squad.Substitutes)
            {                
                // Get from playercache.
                var detailsResponse = await PlayerCache.Instance.Lookup(p, team.TeamId, leagueId);
                var pRatings = ratings.GetValueOrDefault(p.PlayerId) ?? null;
                var validRating = pRatings == null ? false : double.TryParse(pRatings.Rating, out ratingAmount);
                
                if(detailsResponse != null && detailsResponse.Response != null)
                {
                    var pDetails = detailsResponse.Response;
                    
                    // Create player request
                    var player = new PlayerRequest() { 
                        Name = string.Concat(pDetails.FirstName, " ", pDetails.LastName),
                        DateOfBirth = ConvertIsoDatetime(pDetails.BirthDate),
                        Position = "S",
                        ActiveInEvents = new List<MatchEventRequest>(),
                        Height = RemoveMeasurement(pDetails.Height),
                        HeightType = MeasurementType.Cm,
                        Weight = RemoveMeasurement(pDetails.Weight),
                        WeightType = MeasurementType.Kg,        
                        Rating = new MatchRatings(
                            pRatings?.MinutesPlayed ?? 0,
                            validRating ? ratingAmount : 0,
                            pRatings?.Passes.Total ?? 0,
                            pRatings?.Passes.Key ?? 0,
                            pRatings?.Passes.Accuracy ?? 0,
                            pRatings?.Shots.Total ?? 0,
                            pRatings?.Shots.OnTarget ?? 0,
                            pRatings?.Tackles.Total ?? 0,
                            pRatings?.Tackles.Blocks ?? 0,
                            pRatings?.Tackles.Interceptions ?? 0,
                            pRatings?.Dribbles.Attempts ?? 0,
                            pRatings?.Dribbles.Success ?? 0,
                            pRatings?.Dribbles.Past ?? 0,
                            pRatings?.Fouls.Committed ?? 0,
                            pRatings?.Fouls.Drawn ?? 0
                        )
                    };

                    // Temporarily store lookup.
                    requestPlayers.Add(p.PlayerId, player);
                    playersFound.Add(p.PlayerId, player);
                }               
            }
        
            // Create the club
            var c = new ClubRequest();
            c.Name = team.Name;
            c.Players = requestPlayers.Values.ToList();
            
            return c;
        }

        private static string GetPositionFromShortcut(string shortcut)
        {
            switch(shortcut)
            {
                case "G":
                    return "Goalkeeper";
                case "D":
                    return "Defender";
                case "M":
                    return "Midfielder";
                case "A":
                    return "Attacker";
                case "S":
                    return "Substitute";
                default:
                    return "Unknown";
            }
        }

        private static DateTime ConvertIsoDatetime(string standard)
        {
            var formatDate = standard.Split('/');
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
            var sRepresentation = sb.ToString();

            return DateTime.ParseExact(
                sRepresentation, 
                "dd/MM/yyyy", 
                CultureInfo.InvariantCulture);
        }

        private async Task<PlayerDetailResult> PlayerDetailRequest(int playerId, int teamId)
        {   
            Console.Write("Trying to fetch player details for {0} on team {1}", playerId, teamId);         
            var response = await this._players.GetPlayerAsync(teamId, this._season);
            Console.WriteLine("... found {0}", response.Api.Players.Count > 0 ?
                 response.Api.Players[0].FirstName + " " + response.Api.Players[0].LastName : "Nothing");
            return response;
        }
    }    
}