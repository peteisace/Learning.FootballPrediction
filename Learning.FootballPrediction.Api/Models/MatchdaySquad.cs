using System.Collections;
using System.Collections.Generic;

namespace Learning.FootballPrediction.Api.Models
{
    public class MatchdaySquad : IEnumerable<SquadMember>
    {
        private List<SquadMember> _squad = new List<SquadMember>();
        private int _parentId;
        private MatchRole _role;
        
        public MatchdaySquad(int parentId, MatchRole role)
        {
            this._parentId = parentId;
            this._role = role;
        }

        public int ParentID => this._parentId;
        public MatchRole Role => this._role;

        public SquadMember AddNew(Player player, Position position)
        {
            var squadMember = new SquadMember(player, position);            
            return this.AddNew(squadMember);
        }

        public SquadMember AddNew(SquadMember squadMember)
        {
            this._squad.Add(squadMember);

            return squadMember;
        }

        public IEnumerator<SquadMember> GetEnumerator()
        {
            return ((IEnumerable<SquadMember>)_squad).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<SquadMember>)_squad).GetEnumerator();
        }
    }
}