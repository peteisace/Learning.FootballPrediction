using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;
using Microsoft.Extensions.Configuration;
using Peteisace.DataAccess.Client;

namespace Learning.FootballPrediction.Api.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private ModelCache<string, Position> _positionCache = new ModelCache<string, Position>();
        private string _cString;
        private SquadRepository _squadRepo;
        private Dictionary<PlayerRequest, Player> _map = new Dictionary<PlayerRequest, Player>();

        public PlayerRepository(IConfiguration configuration)
        {
            this._cString = configuration.GetConnectionString("default");
            this._squadRepo = new SquadRepository(configuration);
        }

        public async Task<Position> GetPositionByName(string name)
        {
            var item = this._positionCache.GetValue(name);

            if(item == null)
            {
                await DBExecutor.ExecuteReader(this._cString, "dbo.position_selectByName", (reader) => {
                    if(reader.Read())
                    {
                        item = new Position(reader.GetByte("id"), reader.GetString("name"));
                        this._positionCache.Add(name, item);
                    }                    
                }, name);
            }

            // Can still be null.
            return item;
        }

        public async Task<Position> SavePosition(string name)
        {
            var item = this._positionCache.GetValue(name);

            if(item != null)
            {
                throw new ArgumentException("A position {name} already exists.", nameof(name));
            }

            var id = (int)await DBExecutor.ExecuteScalar(this._cString, "dbo.position_insert", name);
            item = new Position((byte)id, name);
            this._positionCache.Add(name, item);

            return item;
        }

        public async Task<Player> GetPlayerByName(PlayerRequest player)
        {
            var nameHash = string.Concat(player.Name, player.DateOfBirth.ToShortDateString()).GetHashCode(); 
            var result = await this._squadRepo.Get(nameHash);

            if(result != null)
            {
                // we can map
                this._map.Add(player, result);
            }

            return result;
        }

        public async Task<Player> SavePlayer(PlayerRequest request)
        {
            if(this._map.ContainsKey(request))
            {
                // should never happen.
            }

            var result = await this._squadRepo.Save(request, request.NameHash);            
            this._map.Add(request, result);

            return result;
        }

        public async Task SaveSquad(MatchdaySquad squadMembers)
        {
            await this._squadRepo.SaveSquad(squadMembers);
        }

        public Player Map(PlayerRequest request)
        {        
            if(this._map.ContainsKey(request))
            {
                return this._map[request];
            }

            return null;
        }
    }
}