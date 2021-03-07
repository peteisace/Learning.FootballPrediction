using System;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class MatchResponse
    {
        public int Id
        {
            get;
            set;
        }

        public DateTime UtcDate
        {
            get;
            set;
        }

        public int Attendance
        {
            get;
            set;
        }

        public int MatchDay
        {
            get;
            set;
        }       

        public TeamResponse HomeTeam
        {
            get;
            set;
        }

        public TeamResponse AwayTeam
        {
            get;
            set;
        }

        public GoalResponse[] Goals
        {
            get;
            set;
        }

        public SubstitutionResponse[] Substitutions
        {
            get;
            set;
        }
    }
}