using System;
using System.Threading;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Source;

namespace Learning.FootballPrediction.DataFetch
{
    class Program
    {
        private const string URI = "http://localhost:4990/v2/competitions/{0}/Matches?season={1}&matchday={2}";
        public static async Task Main(string[] args)
        {
            var matchsource = new MatchSource(URI);
            var c = await matchsource.GetCompetitionAsync(MatchSourceOptions.Default);
            
            foreach(var m in c.Matches)
            {
                Console.WriteLine($"Score was {m.ScoreContainer.Result.Home}-{m.ScoreContainer.Result.Away} in the " + 
                    $"match between {m.HomeTeam.Name} vs {m.AwayTeam.Name}");
            }
        }
    }
}
