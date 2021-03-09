using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learning.FootballPrediction.DataFetch.Api.Source
{
    public class MatchSource
    {
        private string _uri;
        private const string API_KEY = "0550c9f7be9c4d43ba00e6472a115ba1";

        public MatchSource(string uri)
        {
            this._uri = uri;
        }
        public async Task<Competition> GetCompetitionAsync(MatchSourceOptions options = null)
        {
            options = options ?? MatchSourceOptions.Default;            
            HttpWebRequest req = HttpWebRequest.CreateHttp(string.Format(this._uri, options.CompetitionID, options.SeasonStartYear, options.MatchDay));
            req.Headers["X-Auth-Token"] = API_KEY;
            
            using(var resp = await req.GetResponseAsync())
            {
                using(var rStream = resp.GetResponseStream())
                {
                    // Deserialize
                    var comp = await JsonSerializer.DeserializeAsync<Competition>(rStream);
                    return comp;
                }
            }            
        }

        public Competition GetCompetition(MatchSourceOptions options = null)
        {
            options = options ?? MatchSourceOptions.Default;            
            HttpWebRequest req = HttpWebRequest.CreateHttp(string.Format(this._uri, options.CompetitionID, options.SeasonStartYear, options.MatchDay));
            req.Headers["X-Auth-Token"] = API_KEY;
            
            using(var resp = req.GetResponse())
            {
                using(var rStream = resp.GetResponseStream())
                {
                    using(var reader = new StreamReader(rStream))
                    {
                        var json = reader.ReadToEnd();
                        // Deserialize                    
                        var comp = JsonSerializer.Deserialize<Competition>(json);
                        return comp;
                    }
                }
            }            
        }
    }
}