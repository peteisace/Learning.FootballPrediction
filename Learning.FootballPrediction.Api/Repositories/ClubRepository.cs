using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;
using Microsoft.Extensions.Configuration;
using Peteisace.DataAccess.Client;

namespace Learning.FootballPrediction.Api.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private string _connectionString;

        public ClubRepository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("default");
            System.Console.WriteLine("Value of connectionstring is {0}", this._connectionString);
        }

        public async Task<Club> GetClubByName(string name)
        {
            Club found = null;
            await DBExecutor.ExecuteReader(this._connectionString, "dbo.club_selectByName", (reader) => 
            {
                if(reader.Read())
                {
                    found = new Club(reader.GetInt32("id"), reader.GetString("name"));
                }
            }, name);

            return found;
        }

        public async Task<Club> Save(ClubRequest club)
        {
            int id = (int)await DBExecutor.ExecuteScalar(this._connectionString, "dbo.club_insert", club.Name);
            return new Club(id, club.Name);
        }
    }
}