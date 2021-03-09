namespace Learning.FootballPrediction.DataFetch.Api.Destination
{
    public class GoalResponse
    {
        public int Minute
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public EntitySummaryResponse Team   
        {
            get;
            set;
        }

        public EntitySummaryResponse Scorer
        {
            get;
            set;
        }
    }
}