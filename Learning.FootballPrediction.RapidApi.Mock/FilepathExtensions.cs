using Microsoft.AspNetCore.Builder;

namespace Learning.FootballPrediction.RapidApi.Mock
{
    public static class FilepathExtensions
    {
        public static IApplicationBuilder UseJsonContentByUrl(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FilepathMiddleware>();
        }
    }
}