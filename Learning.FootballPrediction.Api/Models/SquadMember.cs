namespace Learning.FootballPrediction.Api.Models
{
    public class SquadMember
    {
        private Player _player;
        private Position _position;

        public SquadMember(Player player, Position position)
        {
            this._player = player;
            this._position = position;
        }

        public Player Player => _player;
        public Position Position => _position;
    }
}