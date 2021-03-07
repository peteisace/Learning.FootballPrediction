namespace Learning.FootballPrediction.DataFetch
{
    // This guy's job is to manage the workflow: for startSeason until current
    //    for 1 until matchdays.End
    //        Grab all the matches for matchDay from Api/Source
    //        Saving them into Api/Destination
    public class Conductor
    {
        private int _startSeason;

        public Conductor(int startSeason)
        {
            this._startSeason = startSeason;
        }

        public void Begin() // journal? // options?
        {

        }
    }
}