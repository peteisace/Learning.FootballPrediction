using System;

namespace Learning.FootballPrediction.Api.Models
{
    public class Player
    {
        private int _id;
        private string _name;
        private DateTime _dob;

        public Player(int id, string name, DateTime dob)
        {
            this._id = id;
            this._name = name;
            this._dob = dob;        
        }

        public int ID => this._id;
        public string Name => this._name;
        public DateTime DateOfBirth => this._dob;
    }
}