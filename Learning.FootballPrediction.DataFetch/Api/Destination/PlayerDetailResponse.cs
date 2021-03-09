using System;

namespace Learning.FootballPrediction.DataFetch.Api.Destination
{
    public class PlayerDetailResponse
    {
        public int ID
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public DateTime DateOfBirth
        {
            get;
            set;
        }

        public string Nationality
        {
            get;
            set;
        }

        public string Position
        {
            get;
            set;
        }
    }
}