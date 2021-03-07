using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Services
{
    public class AggregateClubs : ServiceStep
    {
        private IClubRepository _repo;
        public AggregateClubs(int sequence, IClubRepository repository) : base(sequence)
        {
            this._repo = repository;
        }

        protected override async Task<object> Execute(MatchRequest request, object last)
        {
            // Get the data
            Club[] clubs = new Club[] { 
                await this._repo.GetClubByName(request.Home.Name), 
                await this._repo.GetClubByName(request.Away.Name) 
            };

            clubs[0] = await this.CheckExists(clubs[0], request.Home);
            clubs[1] = await this.CheckExists(clubs[1], request.Away);

            return clubs;
        }

        private async Task<Club> CheckExists(Club club, ClubRequest original)
        {
            if(club == null)
            {
                club = await this._repo.Save(original);
            }

            return club;
        }
    }
}