using System;

namespace Learning.FootballPrediction.Api.Models
{
    public class CreatePlayer
    {
        private string _name;
        private DateTime _dob;
        public CreatePlayer(PlayerRequest request)
        {
            this._name = request.Name;
            this._dob = request.DateOfBirth;
        }

        public string Name => this._name;
        public DateTime Dob => this._dob;
    }
}