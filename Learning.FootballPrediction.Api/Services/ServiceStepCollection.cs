using System.Collections.Generic;
using System.Threading.Tasks;
using Learning.FootballPrediction.Api.Models;

namespace Learning.FootballPrediction.Api.Services
{
    public class ServiceStepCollection
    {
        private List<ServiceStep> _steps = new List<ServiceStep>();

        public int Count=> this._steps.Count;

        public void Add(ServiceStep step)
        {
            this._steps.Add(step);
        }

        public async Task<StepEntry[]> Execute(MatchRequest request)
        {
            // Go through the steps
            List<StepEntry> entries = new List<StepEntry>();

            foreach(var step in this._steps)
            {         
                var entry = await step.Run(request, entries.Count == 0 ? null : entries[entries.Count - 1].StepResult);
                entries.Add(entry);
            }

            return entries.ToArray();
        }
    }
}