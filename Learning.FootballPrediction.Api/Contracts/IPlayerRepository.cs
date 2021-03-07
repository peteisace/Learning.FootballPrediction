using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Contracts
{
    public interface IPlayerRepository
    {
        Task<Position> GetPositionByName(string name);

        Task<Position> SavePosition(string name);

        Task<Player> GetPlayerByName(PlayerRequest name);

        Task<Player> SavePlayer(PlayerRequest request);

        Task SaveSquad(MatchdaySquad squad);

        Player Map(PlayerRequest request);
    }
}