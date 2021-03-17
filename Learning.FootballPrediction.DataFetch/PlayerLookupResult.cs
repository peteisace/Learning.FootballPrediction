using Learning.FootballPrediction.DataFetch.Api.Source;

namespace Learning.FootballPrediction.DataFetch
{
    public class PlayerLookupResult
    {
        private bool _found;
        private PlayerDetailResponse _response;

        public PlayerLookupResult(bool found, PlayerDetailResponse response)
        {
            this._found = found;
            this._response = response;
        }

        public bool Found => this._found;

        public PlayerDetailResponse Response => this._response;
    }
}