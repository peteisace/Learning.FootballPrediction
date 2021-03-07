using System;

namespace Learning.FootballPrediction.Api.Models
{
    public class MatchRequest
    {
        private DateTime _played;

        private ClubRequest _home;
        private ClubRequest _away;

        public DateTime Played
        {
            get => this._played;
            set => this._played = value;
        }

        public ClubRequest Home
        {
            get => this._home;
            set => this._home = value;
        }

        public ClubRequest Away
        {
            get => this._away;
            set => this._away = value;
        }
    }
}