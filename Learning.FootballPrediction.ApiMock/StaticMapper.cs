using System.Text.RegularExpressions;

namespace Learning.FootballPrediction.ApiMock
{
    public static class StaticMapper
    {
        private const string APIKEY = "0550c9f7be9c4d43ba00e6472a115ba1";
        private const string FORMAT = @"\/v2/(?<location>\w+)\/(?<parameter>\w+)(\/(?<location2>\w+))?";

        public static string GetFileLocation(string requestUrl)
        {
            var matches = Regex.Matches(requestUrl, FORMAT);
            if(matches.Count == 1 && matches[0].Groups.Count >= 2)
            {
                var lGroup = matches[0].Groups["location"];
                Group ending = null;
                var endPart = matches[0].Groups.TryGetValue("location2", out ending) ? ending.Value : string.Empty;
                return lGroup.Success ? $"json/{lGroup.Value}{endPart}.json" : null;
            }

            return null;
        }
    }
}