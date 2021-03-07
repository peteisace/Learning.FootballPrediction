using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Services
{
    public abstract class ServiceStep
    {
        private readonly int _sequence;

        protected ServiceStep(int sequence)
        {
            this._sequence = sequence;
        }

        public int Sequence => this._sequence;

        public async Task<StepEntry> Run(MatchRequest request, object last)
        {
            var w = Stopwatch.StartNew();
            object result = null;

            result = await this.Execute(request, last);
            w.Stop();

            var info = new StepEntry(this._sequence);
            info.StepResult = result;
            info.TimeTaken = (int)w.Elapsed.TotalMilliseconds;

            return info;
        }

        protected abstract Task<object> Execute(MatchRequest request, object last);
    }
}