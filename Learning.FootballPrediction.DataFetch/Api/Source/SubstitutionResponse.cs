namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class SubstitutionResponse
    {
        public int Minute
        {
            get;
            set;
        }

        public EntitySummaryResponse Team
        {
            get;
            set;
        }

        public EntitySummaryResponse PlayerIn
        {
            get;
            set;
        }

        public EntitySummaryResponse PlayerOut
        {
            get;
            set;
        }
    }
}