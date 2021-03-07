using System;

namespace Learning .FootballPrediction.Api.Models
{
    public class Match
    {
        private int _id;
        private DateTime _played;

        private Club _home;

        private Club _away;

        private MatchEvents _matchEvents;

        public Match(int id, CreateMatch request) : this(id, request.Original.Played, request.Home, request.Away)
        {
        }

        public Match(int id, DateTime played, Club home, Club away)
        {
            this._id = id;
            this._played = played;
            this._home = home;
            this._away = away;
            this._home.Squad = new MatchdaySquad(this._id, MatchRole.HomeClub);
            this._away.Squad = new MatchdaySquad(this._id, MatchRole.AwayClub);
            this._matchEvents = new MatchEvents(this._id);
        }


        public int ID => this._id;
        public Club Home => this._home;
        public Club Away => this._away;
        public DateTime Played => this._played;
        public MatchEvents MatchEvents => this._matchEvents;
    }
}