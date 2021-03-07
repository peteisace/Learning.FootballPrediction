namespace Learning.FootballPrediction.Api.Models
{
    public class MatchEvent
    {
        private int _playerId;

        private byte? _minute;
        private EventType _type;

        public MatchEvent(int playerId, byte? minute, EventType eventType)
        {
            this._playerId = playerId;
            this._minute = minute;
            this._type  = eventType;
        }

        public byte? Minute => this._minute;
        public EventType Type => this._type;

        public int PlayerID => this._playerId;
    }
}