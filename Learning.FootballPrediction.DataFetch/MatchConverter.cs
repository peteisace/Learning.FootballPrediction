using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using src = Learning.FootballPrediction.DataFetch.Api.Source;
using System.Linq;

namespace Learning.FootballPrediction.DataFetch
{
    public class MatchConverter
    {
        public MatchRequest ToMatch(MatchResponse response)
        {
            // Create the container.
            var m = new MatchRequest();
            m.Played = response.UtcDate;

            // Set up the teams.
        }

        public ClubRequest ToClub(src.TeamResponse team)
        {
            // Create the container
            var c = new ClubRequest(team.Name);
        }
    }    
}