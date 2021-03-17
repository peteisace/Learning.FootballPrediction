using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Source;

namespace Learning.FootballPrediction.DataFetch
{
    public class PlayerCache
    {
        public delegate Task<PlayerDetailResponse> PlayerLookupHandler(int id);
        private List<WeakReference<PlayerLookupHandler>> _subs = new List<WeakReference<PlayerLookupHandler>>();
        private Dictionary<int, PlayerDetailResponse> _cache = new Dictionary<int, PlayerDetailResponse>();

        private static PlayerCache _instance = new PlayerCache();

        private PlayerCache()
        {
        }

        public static PlayerCache Instance
        {
            get { return _instance; }
        }

        public async Task<PlayerLookupResult> Lookup(PlayerResponse incoming)
        {
            var k = string.Concat(
                incoming.Id
            );

            PlayerDetailResponse returned = null;

            if(!this._cache.ContainsKey(incoming.Id))
            {
                foreach(var s in this._subs)
                {
                    PlayerLookupHandler target = null;
                    if(s.TryGetTarget(out target))
                    {
                        returned = await target.Invoke(incoming.Id);
                        return new PlayerLookupResult(false, returned);
                    }
                }
            }

            // Send it back.
            return new PlayerLookupResult(true, this._cache.GetValueOrDefault(incoming.Id, null));
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