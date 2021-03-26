using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Rapid;

namespace Learning.FootballPrediction.DataFetch
{
    public class PlayerCache
    {
        public delegate Task<PlayerDetailResult> PlayerLookupHandler(int id);
        private List<WeakReference<PlayerLookupHandler>> _subs = new List<WeakReference<PlayerLookupHandler>>();
        private Dictionary<int, PlayerDetails> _cache = new Dictionary<int, PlayerDetails>();

        private static PlayerCache _instance = new PlayerCache();

        private PlayerCache()
        {
        }

        public static PlayerCache Instance
        {
            get { return _instance; }
        }

        public async Task<PlayerLookupResult> Lookup(SquadInfo incoming, int leagueId)
        {
            var k = string.Concat(
                incoming.PlayerId
            );

            PlayerDetails returned = null;

            if(!this._cache.ContainsKey(incoming.PlayerId))
            {
                foreach(var s in this._subs)
                {
                    PlayerLookupHandler target = null;
                    if(s.TryGetTarget(out target))
                    {
                        var list = await target.Invoke(incoming.PlayerId);
                        // Process the list.
                        foreach(var p in list.Api.Players)
                        {
                            if(p.LeagueId.HasValue && p.LeagueId.Value == leagueId && !this._cache.ContainsKey(p.PlayerId))
                            {
                                // Add it to the cache.  
                                this._cache.Add(p.PlayerId, p);
                            }
                        }

                        // Returned
                        returned = this._cache.GetValueOrDefault(incoming.PlayerId);
                        return new PlayerLookupResult(false, returned);
                    }
                }
            }

            // Send it back.
            return new PlayerLookupResult(true, this._cache.GetValueOrDefault(incoming.PlayerId, null));
        }

        public bool RegisterHandler(PlayerLookupHandler handler)
        {
            var wr = new WeakReference<PlayerLookupHandler>(handler);
            if(!this._subs.Contains(wr))
            {
                this._subs.Add(wr);
                return true;
            }

            return false;
        }
    }
}