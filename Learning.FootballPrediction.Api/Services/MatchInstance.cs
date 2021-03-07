using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Services
{
    public class MatchInstance : ServiceStep
    {
        private readonly IMatchRepository _repository;

        public MatchInstance(int sequence, IMatchRepository repository) : base(sequence) 
        {
            this._repository = repository;
        }
        protected override async Task<object> Execute(MatchRequest request, object last)        
        {
            Club[] clubs = (Club[])last;
            // Create our match model
            CreateMatch m = new CreateMatch(request, clubs[0], clubs[1]);

            // Persist it.
            var match = await this._repository.Save(m);
            
            return match;
        }
    }
}