namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class MatchSourceOptions
    {
        public int SeasonStartYear
        {
            get;
            set;
        }

        public int CompetitionID
        {
            get;
            set;
        }

        public int MatchDay
        {
            get;
            set;
        }

        public static MatchSourceOptions Default
        {
            get 
            { 
                return new MatchSourceOptions() { 
                    SeasonStartYear = 2019, 
                    CompetitionID = 2021, 
                    MatchDay = 1
                }; 
            }
        }
    }
}