using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Services
{
    public class MatchCreator
    {
        private ServiceStepCollection _steps = new ServiceStepCollection();

        public MatchCreator(IClubRepository clubRepository, IMatchRepository matchRepository, IPlayerRepository playerRepository)
        {
            var wf = PrepareWorkflow(clubRepository, matchRepository, playerRepository);
            foreach(var ss in wf)
            {
                this._steps.Add(ss);
            }            
        }

        public async Task<Match> RunWorkflow(MatchRequest request)
        {
            // Get how we did.
            var journal = await this._steps.Execute(request);

            // Last item will have our returned object
            var last = journal[journal.Length - 1];

            return (Match)last.StepResult;
        }

        private static ServiceStep[] PrepareWorkflow(IClubRepository clubRepository, IMatchRepository matchRepo, IPlayerRepository playerRepository)
        {
            return new ServiceStep[] 
            {
                new Initial(),
                new AggregateClubs(1, clubRepository),
                new MatchInstance(2, matchRepo),
                new CreatePlayers(3, playerRepository),
                new AddEvents(4, matchRepo, playerRepository)
            };
        }

        private class Initial : ServiceStep
        {
            public Initial() : base(0)
            {
            }

            protected async override Task<object> Execute(MatchRequest request, object last)
            {
                await Task.Yield();
                return null;
            }
        }
    }
}