using System.Collections;
using System.Collections.Generic;

namespace Learning.FootballPrediction.Api.Models
{
    public class MatchEvents : IEnumerable<MatchEvent>
    {
        private List<MatchEvent> _source = new List<MatchEvent>();
        private int _parentId;

        public MatchEvents(int parentId)
        {
            this._parentId = parentId;
        }

        public int MatchID => this._parentId;
        public int Count => this._source.Count;

        public MatchEvent AddNew(Player player, MatchEventRequest request)
        {
            var ev = new MatchEvent(player.ID, request.Minute, request.Type);
            this._source.Add(ev);

            return ev;
        }

        public IEnumerator<MatchEvent> GetEnumerator()
        {
            return ((IEnumerable<MatchEvent>)_source).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<MatchEvent>)_source).GetEnumerator();
        }
    }
}