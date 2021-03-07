using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Models;
using System.Linq;
using System.Collections.Generic;
using Learning.FootballPrediction.Api.Contracts;

namespace Learning.FootballPrediction.Api.Services
{
    public class AggregatePositions : ServiceStep
    {
        private IPlayerRepository _repo;
        public AggregatePositions(int sequence, IPlayerRepository playerRepository) : base(sequence)
        {
            this._repo = playerRepository;
        }

        protected override async Task<object> Execute(MatchRequest request, object last)
        {
            // Take the squads and get distinct positions.            
            var result = FindDistinctPositions(request.Home);
            var merge = FindDistinctPositions(request.Away);
            
            foreach(var position in merge)
            {
                if(!result.Contains(position))
                {
                    result.Add(position);
                }
            }

            // Place into Position objects
            Position[] positions = new Position[result.Count];

            for(int i = 0; i < positions.Length; i++)
            {
                var positionInstance = await this._repo.GetPositionByName(result[i]);
                if(positionInstance == null)
                {
                    positionInstance = await this._repo.SavePosition(result[i]);
                }

                positions[i] = positionInstance;
            }
            
            // Send back a dictionary of positions - via name, not ID.
            return positions.ToArray();
        }

        private static List<string> FindDistinctPositions(ClubRequest club)
        {
            List<string> pos = new List<string>();
            foreach(var p in club.Players)
            {
                if(!pos.Contains(p.Position))
                {
                    pos.Add(p.Position);
                }
            }

            return pos;
        }
    }
}