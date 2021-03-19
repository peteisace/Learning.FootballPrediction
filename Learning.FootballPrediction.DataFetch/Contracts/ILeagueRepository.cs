using Learning.FootballPrediction.DataFetch.Api.Rapid;

namespace Learning.FootballPrediction.DataFetch.Contracts
{
    public interface ILeagueRepository
    {
        LeagueResult GetEnglishLeagues();
    }
}