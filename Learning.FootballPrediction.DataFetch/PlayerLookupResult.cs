using Learning.FootballPrediction.DataFetch.Api.Rapid;

namespace Learning.FootballPrediction.DataFetch
{
    public class PlayerLookupResult
    {
        private bool _found;
        private PlayerDetails _response;

        public PlayerLookupResult(bool found, PlayerDetails response)
        {
            this._found = found;
            this._response = response;
        }

        public bool Found => this._found;

        public PlayerDetails Response => this._response;
    }
}