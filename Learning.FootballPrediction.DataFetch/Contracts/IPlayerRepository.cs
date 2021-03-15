using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Source;

namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface IPlayerRepository
    {
        Task<PlayerResponse> GetPlayerAsync(int id);
    }
}