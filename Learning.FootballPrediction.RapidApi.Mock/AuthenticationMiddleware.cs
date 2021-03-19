using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Learning.FootballPrediction.RapidApi.Mock
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY = "e06d0cae13msh869d4ec18b861afp1052afjsn9c140eb5c35b";
        private const string API_HOST = "api-football-v1.p.rapidapi.com";

        private const string API_HEADER = "x-rapidapi-key";
        private const string HOST_HEADER = "x-rapidapi-host";

        public AuthenticationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Grab out of headers.
            if(context.Request.Headers.ContainsKey(API_HEADER) && context.Request.Headers.ContainsKey(API_HOST))
            {
                StringValues headerValues;
                // we have the headers, check the values.
                if(context.Request.Headers.TryGetValue(API_HEADER, out headerValues))
                {
                    if(headerValues == API_KEY && context.Request.Headers.TryGetValue(HOST_HEADER, out headerValues))
                    {
                        if(headerValues == API_HOST)
                        {
                            await this._next.Invoke(context);
                            return;
                        }
                    }
                }
            }
            
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid security - api key or api host not sent, or wrong values");
            
        }
    }
}