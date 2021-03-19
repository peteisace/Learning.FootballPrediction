using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Learning.FootballPrediction.DataFetch.Api.Rapid
{
    public class TeamSquadDetails
    {
        [JsonPropertyName("startXI")]
        public List<SquadInfo> StartXi
        {
            get;
            set;
        }

        [JsonPropertyName("substitutes")]
        public List<SquadInfo> Substitutes
        {
            get;
            set;
        }
    }
}