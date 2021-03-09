using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Learning.FootballPrediction.ApiMock
{
    public class MappedFileMiddleware
    {
        private readonly RequestDelegate _next;

        public MappedFileMiddleware(RequestDelegate next) 
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request);
            System.Console.WriteLine(url);
            var mapped = StaticMapper.GetFileLocation(url);

            if(mapped == null)
            {
                await this._next(context);                
                return;
            }

            // Otherwise...
            string contents = await File.ReadAllTextAsync(mapped);
            await context.Response.WriteAsync(contents);
        }
    }
}