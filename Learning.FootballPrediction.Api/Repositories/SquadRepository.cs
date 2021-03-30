using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Models;
using Microsoft.Extensions.Configuration;
using Peteisace.DataAccess.Client;

namespace Learning.FootballPrediction.Api.Repositories
{
    public class SquadRepository : CachedRepositoryBase<int, Player, PlayerRequest>
    {
        public SquadRepository(IConfiguration configuration) : base(configuration)
        {            
        }

        public async Task SaveSquad(MatchdaySquad squad)
        {
            foreach(var p in squad)
            {
                await this.SaveSquadMember(squad.ParentID, squad.Role, p.Player.ID, p.Position.ID);
            }
        }
        private async Task SaveSquadMember(int matchId, MatchRole matchRole, int playerId, byte positionId)
        {
            // model is unaffected.
            await DBExecutor.ExecuteNonQuery(
                this.ConnectionString, 
                "dbo.squadMember_insert",
                matchId,
                (byte)matchRole,
                playerId,
                positionId);
        }

        protected async override Task<Player> FetchFromStore(int key)
        {
            Player p = null;
            await DBExecutor.ExecuteReader(this.ConnectionString, "dbo.player_select", (reader) => 
            {
                if(reader.Read())                
                {
                    p = new Player(
                        reader.GetInt32("id"),
                        reader.GetString("full_name"),
                        reader.GetDateTime("date_of_birth")
                    );
                }
            }, key);

            return p;
        }

        protected async override Task<Player> SaveDetailed(PlayerRequest model)
        {
            System.Console.WriteLine($"Currently saving player: ({model.Name}) - born: {model.DateOfBirth}, hash: {model.NameHash}");
            Player p = null;
            var newId = (int)await DBExecutor.ExecuteScalar(
                this.ConnectionString, 
                "dbo.player_insert", 
                model.Name, 
                model.DateOfBirth, 
                model.NameHash);
            
            p = new Player(newId, model.Name, model.DateOfBirth);
            return p;
        }
    }
}