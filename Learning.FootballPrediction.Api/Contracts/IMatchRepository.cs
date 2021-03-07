using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Contracts
{
    public interface IMatchRepository
    {
        Task<Match> Save(CreateMatch request);

        Task SaveEvents(MatchEvents matchEvents);
    }
}