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
                await this.SaveSquadMember(squad.ParentID, squad.Role, p);
            }
        }
        private async Task SaveSquadMember(int matchId, MatchRole matchRole, SquadMember p)
        {
            // model is unaffected.
            await DBExecutor.ExecuteNonQuery(
                this.ConnectionString, 
                "dbo.squadMember_insert",
                matchId,
                (byte)matchRole,
                p.Player.ID,
                p.Position.ID,
                p.Ratings.MinutesPlayed,
                p.Ratings.Rating,
                p.Ratings.Passes,
                p.Ratings.Key,
                p.Ratings.Accuracy,
                p.Ratings.Shots,
                p.Ratings.OnTarget,
                p.Ratings.Tackles,
                p.Ratings.Blocks,
                p.Ratings.Interceptions,
                p.Ratings.Dribbles,
                p.Ratings.Success,
                p.Ratings.Past,
                p.Ratings.FoulsCommitted,
                p.Ratings.FoulsDrawn);
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
                        reader.GetDateTime("date_of_birth"),
                        0,
                        MeasurementType.cm,
                        0,
                        MeasurementType.kg
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
                model.NameHash,
                model.Height,
                model.HeightType,
                model.Weight,
                model.WeightType);
            
            p = new Player(newId, model.Name, model.DateOfBirth, model.Height, model.HeightType, model.Weight, model.WeightType);
            return p;
        }
    }
}