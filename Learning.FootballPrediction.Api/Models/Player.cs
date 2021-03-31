using System;

namespace Learning.FootballPrediction.Api.Models
{
    public class Player
    {
        private int _id;
        private string _name;
        private DateTime _dob;
        private int? _weight;
        private int? _height;
        private MeasurementType _weightType;
        private MeasurementType _heightType;

        public Player(
            int id, 
            string name, 
            DateTime dob, 
            int? height, 
            MeasurementType heightType, 
            int? weight, 
            MeasurementType weightType)
        {
            this._id = id;
            this._name = name;
            this._dob = dob;    
            this._height = height;
            this._heightType = heightType;
            this._weight = weight;
            this._weightType = weightType;
        }

        public int ID => this._id;
        public string Name => this._name;
        public DateTime DateOfBirth => this._dob;
        public int? Height => this._height;
        public int? Weight => this._weight;
        public MeasurementType HeightType => this._heightType;
        public MeasurementType WeightType => this._weightType;
    }
}