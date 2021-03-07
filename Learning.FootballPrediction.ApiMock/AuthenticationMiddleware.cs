using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Learning.FootballPrediction.ApiMock
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "0550c9f7be9c4d43ba00e6472a115ba1";
        public AuthenticationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            StringValues sv;
            // check the request for parameters
            if(context.Request.Headers.TryGetValue("X-Auth-Token", out sv))
            {
                if(sv == APIKEY)
                {
                    // we're good and can move on
                    await this._next.Invoke(context);
                    return;
                }
            }

            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Didn't supply correct auth-token.");

            // complete
        }
    }
}