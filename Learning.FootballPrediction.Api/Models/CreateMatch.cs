namespace Learning.FootballPrediction.Api.Models
{
    public class CreateMatch
    {
        private MatchRequest _original;
        private Club _home;
        private Club _away;

        public CreateMatch(MatchRequest request, Club home, Club away)
        {
            this._original = request;
            this._home = home;
            this._away = away;
        }

        public Club Away => this._away;
        public Club Home => this._home;
        public MatchRequest Original => this._original; 
    }
}