using Microsoft.AspNetCore.Builder;

namespace Learning.FootballPrediction.ApiMock
{
    public static class MappingExtensions
    {
        public static IApplicationBuilder UseApiMappings(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MappedFileMiddleware>();
        }
    }
}