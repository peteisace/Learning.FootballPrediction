using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Contracts
{
    public interface IClubRepository
    {
        Task<Club> GetClubByName(string name);

        Task<Club> Save(ClubRequest club);
    }
}