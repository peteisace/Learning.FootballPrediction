using System;

namespace Learning.FootballPrediction.Api.Services
{
    public class StepEntry
    {
        private int _id;

        public StepEntry(int id)
        {
            this._id = id;
        }
        
        public int TimeTaken 
        {
            get;
            set;
        }

        public Exception[] Errors
        {
            get;
            set;
        }

        public object StepResult
        {
            get;
            set;
        }

        public int ID => this._id;        
    }
}