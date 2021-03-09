using System;

namespace Learning.FootballPrediction.DataFetch
{
    // This guy's job is to manage the workflow: for startSeason until current
    //    for 1 until matchdays.End
    //        Grab all the matches for matchDay from Api/Source
    //        Saving them into Api/Destination
    public class Conductor
    {
        // Competition ID for the premier league.
        public const int COMPETITION_ID = 2021;

        // First season of the premier league.
        public const int FIRST_SEASON = 1993;
        private int _startSeason;

        public Conductor(int startSeason)
        {
            // get the date.
            var d = DateTime.Today;
            int endSeason = DateTime.Today.Year -1;
            
            if(startSeason < FIRST_SEASON || startSeason > endSeason)
            {
                throw new ArgumentException($"Seasons must be between {FIRST_SEASON} and {endSeason}");
            }

            this._startSeason = startSeason;
        }

        public void Begin() // journal? // options?
        {
            // so we get a load of matches for the current season.
        }
    }
}