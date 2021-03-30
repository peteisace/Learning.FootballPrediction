using System;

namespace Learning.FootballPrediction.Api.Models
{
    public class PlayerRequest
    {
        private string _name;
        private string _position;

        private DateTime _dob;
        private byte? _height;
        private byte? _weight;
        private MeasurementType _heightType;
        private MeasurementType _weightType;

        private MatchEventRequest[] _activeEvents = new MatchEventRequest[0];
        private MatchRatings _ratings;

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

        public byte? Height
        {
            get => this._height;
            set => this._height = value;
        }
        
        public MeasurementType HeightType
        {
            get => this._heightType;
            set => this._heightType = value;
        }

        public byte? Weight 
        {
            get => this._weight;
            set => this._weight = value;
        }

        public MeasurementType WeightType 
        {
            get => this._weightType;
            set => this._weightType = value;
        }

        public MatchEventRequest[] ActiveInEvents 
        {
            get => this._activeEvents;
            set => this._activeEvents = value;
        }

        public MatchRatings Rating
        {
            get => this._ratings;
            set => this._ratings = value;
        }

        public int NameHash => string.Concat(this._name, this.DateOfBirth.ToShortDateString()).GetHashCode();
    }
}