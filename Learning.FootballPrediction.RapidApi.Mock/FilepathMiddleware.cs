using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Learning.FootballPrediction.RapidApi.Mock
{
    public class FilepathMiddleware
    {
        private RequestDelegate _next;
        private const string GROUP_NAME = "filepath";
        private readonly string PATTERN = $"v2/(\\w+/)+(?<{GROUP_NAME}>\\w+)";

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
                        foreach(Group g in m.Groups)
                        {
                            var path = $"~/json/{g.Value}.json";
                            if(g.Name == GROUP_NAME && File.Exists(path))
                            {
                                // Try and read our value
                                var contents = File.ReadAllText(path);
                                context.Response.ContentType = "appliction/json";
                                await context.Response.WriteAsync(contents);
                            }
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