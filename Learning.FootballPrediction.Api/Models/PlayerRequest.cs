using System;

namespace Learning.FootballPrediction.Api.Models
{
    public class PlayerRequest
    {
        private string _name;
        private string _position;

        private DateTime _dob;

        private MatchEventRequest[] _activeEvents = new MatchEventRequest[0];

        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        public DateTime DateOfBirth
        {
            get => this._dob;
            set => this._dob = value;
        }

        public string Position
        {
            get => this._position;
            set => this._position = value;
        }        

        public MatchEventRequest[] ActiveInEvents 
        {
            get => this._activeEvents;
            set => this._activeEvents = value;
        }

        public int NameHash => this._name?.GetHashCode() ?? -1;
    }
}