namespace Learning.FootballPrediction.Api.Models
{
    public class MatchEventRequest
    {
        private byte? _minute;
        private EventType _type;

        public MatchEventRequest()
        {            
        }

        public MatchEventRequest(byte? minute, EventType eventType)
        {
            this._minute = minute;
            this._type  = eventType;
        }

        public byte? Minute { get => _minute; set => _minute = value; }
        public EventType Type { get => _type; set => _type = value; }
    }
}