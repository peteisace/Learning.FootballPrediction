using Microsoft.AspNetCore.Builder;

namespace Learning.FootballPrediction.ApiMock
{
    public static class AuthenticationExtensions
    {
        public static IApplicationBuilder UseAuthTokenValidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}