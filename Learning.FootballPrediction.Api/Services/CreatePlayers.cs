using System.Threading.Tasks;
using System.Linq;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Services
{
    public class CreatePlayers : ServiceStep
    {
        private IPlayerRepository _playerRepository;
        public CreatePlayers(int sequence, IPlayerRepository playerRepository) : base(sequence)
        {
            this._playerRepository = playerRepository;
        }

        protected override async Task<object> Execute(MatchRequest request, object last)
        {
            // For each list of players, get real models, and create squad            
            Match m = (Match)last;
            await this.AddSquad(request.Home.Players, m.Home.Squad);
            await this.AddSquad(request.Away.Players, m.Away.Squad);

            // And shave.
            await this._playerRepository.SaveSquad(m.Home.Squad);
            await this._playerRepository.SaveSquad(m.Away.Squad);

            return m;
        }        

        private async Task AddSquad(PlayerRequest[] players, MatchdaySquad squad)
        {
            foreach(var p in players)
            {
                var squadMember = await this.CreateSquadMember(p);
                squad.AddNew(squadMember);
            }
        }

        private async Task<SquadMember> CreateSquadMember(PlayerRequest p)
        {
            var position = await this._playerRepository.GetPositionByName(p.Position) ?? await this._playerRepository.SavePosition(p.Position);
            var player = await this._playerRepository.GetPlayerByName(p) ?? await this._playerRepository.SavePlayer(p);

            return new SquadMember(player, position, p.Rating ?? new MatchRatings());
        }
    }
}