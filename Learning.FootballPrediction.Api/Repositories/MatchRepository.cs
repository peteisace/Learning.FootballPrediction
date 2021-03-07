using System;
using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;
using Microsoft.Extensions.Configuration;
using Peteisace.DataAccess.Client;

namespace Learning.FootballPrediction.Api.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private string _cString;
        public MatchRepository(IConfiguration config)
        {
            this._cString = config.GetConnectionString("default");
        }

        public async Task<Match> Save(CreateMatch request)
        {
            int id = (int)await DBExecutor.ExecuteScalar(
                this._cString, 
                "dbo.match_insert", 
                request.Original.Played, 
                request.Home.ID, 
                request.Away.ID);

            return new Match(id, request);
        }

        public async Task SaveEvents(MatchEvents matchEvents)
        {
            foreach(var mEvent in matchEvents)
            {
                Console.WriteLine($"Trying to persist ${mEvent.Type} for player ${mEvent.PlayerID}");
                await DBExecutor.ExecuteNonQuery(
                    this._cString,
                    "dbo.matchevent_insert",
                    matchEvents.MatchID,
                    mEvent.Type,
                    mEvent.PlayerID,
                    mEvent.Minute               
                );
            }
        }
    }
}