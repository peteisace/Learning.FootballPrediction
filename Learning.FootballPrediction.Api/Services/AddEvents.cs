using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Contracts;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Services
{
    public class AddEvents : ServiceStep
    {
        private IMatchRepository _matchRepository;
        private IPlayerRepository _playerRepository;

        public AddEvents(int sequence, IMatchRepository matchRepository, IPlayerRepository playerRepository) : base(sequence)
        {
            this._matchRepository = matchRepository;
            this._playerRepository = playerRepository;
        }

        protected override async Task<object> Execute(MatchRequest request, object last)
        {
            // Grab el matcho.
            Match m = (Match)last;

            // Using request push events            
            this.CreateEvents(request.Home.Players, m);
            this.CreateEvents(request.Away.Players, m);

            // Now save the events.
            await this._matchRepository.SaveEvents(m.MatchEvents);

            return m;
        }

        private void CreateEvents(PlayerRequest[] requests, Match m)
        {
            foreach(var player in requests)
            {
                if(player.ActiveInEvents != null && player.ActiveInEvents.Length > 0)
                {
                    // we must create a match event.
                    // find player instance. We can rely on cache here.
                    var pInstance = this._playerRepository.Map(player);

                    if(pInstance == null)
                    {
                        // can't happen
                    }

                    // Add it to match
                    foreach(var mEvent in player.ActiveInEvents)
                    {
                        m.MatchEvents.AddNew(pInstance, mEvent);
                    }
                }
            }
        }
    }
}