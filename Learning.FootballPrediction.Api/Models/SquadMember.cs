namespace Learning.FootballPrediction.Api.Models
{
    public class SquadMember
    {
        private Player _player;
        private Position _position;
        private MatchRatings _ratings; 

        public SquadMember(Player player, Position position, MatchRatings ratings)
        {
            this._player = player;
            this._position = position;
            this._ratings = ratings;
        }

        public Player Player => _player;
        public Position Position => _position;
        public MatchRatings Ratings => _ratings;
    }
}