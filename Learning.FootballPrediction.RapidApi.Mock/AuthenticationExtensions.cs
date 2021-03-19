using Microsoft.AspNetCore.Builder;

namespace Learning.FootballPrediction.RapidApi.Mock
{
    public static class AuthenticationExtensions
    {
        public static IApplicationBuilder AddTokenAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}