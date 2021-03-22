using System.Collections.Generic;
using IO.Swagger.Model;
using Learning.FootballPrediction.DataFetch.Api.Rapid;
using MatchEvent = Learning.FootballPrediction.DataFetch.Api.Rapid.MatchEvent;

namespace Learning.FootballPrediction.DataFetch
{
    public static class EventConverter
    {
        public static Dictionary<int, List<MatchEventRequest>> Convert(MatchEvent[] matchEvents)
        {
            Dictionary<int, List<MatchEventRequest>> mEvents = new Dictionary<int, List<MatchEventRequest>>();

            foreach(var matchEvent in matchEvents)
            {
                switch(matchEvent.Type)
                {
                    case MatchEventType.Goal:
                        AddRow(matchEvent.PlayerId, new MatchEventRequest(matchEvent.Elapsed, EventType.GoalScored), mEvents);
                        if(matchEvent.AssistId.HasValue)
                        {
                            AddRow(matchEvent.AssistId.Value, new MatchEventRequest(
                                matchEvent.Elapsed,
                                EventType.Assist
                            ), mEvents);
                        }

                        if(matchEvent.Detail == "Penalty")
                        {
                            AddRow(matchEvent.PlayerId, new MatchEventRequest(matchEvent.Elapsed, EventType.PenaltySuccess), mEvents);
                        }
                        break;
                    case MatchEventType.Card:
                        AddRow(matchEvent.PlayerId, new MatchEventRequest(
                            matchEvent.Elapsed, 
                            matchEvent.Detail.StartsWith("Yellow") ? EventType.YellowCard : EventType.RedCard), mEvents);
                        break;
                    case MatchEventType.Susbtitution:
                        AddRow(matchEvent.PlayerId, new MatchEventRequest(
                            matchEvent.Elapsed, 
                            EventType.SubOff), mEvents);
                        if(matchEvent.AssistId.HasValue)
                        {
                            AddRow(matchEvent.AssistId.Value, new MatchEventRequest(
                                matchEvent.Elapsed,
                                EventType.SubOn
                            ), mEvents);
                        }
                        break;
                }
            }

            return mEvents;
        }

        private static void AddRow(int key, MatchEventRequest record, Dictionary<int, List<MatchEventRequest>> current)
        {
            // Check if we have the key
            if(!current.ContainsKey(key))
            {
                current.Add(key, new List<MatchEventRequest>());
            }

            current[key].Add(record);
        }
    }
}