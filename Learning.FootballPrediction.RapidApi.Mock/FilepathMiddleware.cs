using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Learning.FootballPrediction.RapidApi.Mock
{
    public class FilepathMiddleware
    {
        private RequestDelegate _next;

        private const string SUBJECT_NAME = "subject";
        private const string GROUP_NAME = "filepath";
        //  
        private readonly string PATTERN = $"v2/(?<{SUBJECT_NAME}>\\w+)?/(?<{GROUP_NAME}>\\w+)(/\\w+(\\-\\w+)?)*";

        public FilepathMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path.Value.Contains("v2"))
            {
                // grab the json file we care about.
                var matches = Regex.Matches(context.Request.Path.Value, PATTERN);
                
                if(matches.Count >= 1)
                {
                    foreach(Match m in matches)
                    {
                        var subject = m.Groups[SUBJECT_NAME];
                        var group = m.Groups[GROUP_NAME];

                        var p = $"json/{subject?.Value}_{group?.Value}.json";
                        
                        if(File.Exists(p))
                        {
                            // Try and read our value
                            var contents = File.ReadAllText(p);
                            context.Response.ContentType = "application/json; charset=utf-8";
                            context.Response.ContentLength = contents.Length;
                            await context.Response.WriteAsync(contents);
                            return;
                        }
                    }
                }

                // It's a 404.
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("File could not be found.");
            }

            // Just call the next guy.
            await this._next.Invoke(context);
        }
    }
}