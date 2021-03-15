

using System;
using System.IO;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Contracts;
using Learning.FootballPrediction.DataFetch.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Learning.FootballPrediction.DataFetch
{
    class Program
    {
        private const string URI = "http://localhost:4990/v2/competitions/{0}/Matches?season={1}&matchday={2}";
        public static async Task Main(string[] args)
        {
            MatchConfig mConfiguration = null;
            RunConfiguration rConfiguration = null;

            var hostBuilder = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(configBuilder => {
                    if(args != null)
                    {
                        args = new string[0];
                    }
                    
                    configBuilder.AddCommandLine(args);
                    configBuilder.AddEnvironmentVariables();
                    rConfiguration = new RunConfiguration(args);
                })
                .ConfigureAppConfiguration((hostingContext, cfg) => {
                    cfg.AddEnvironmentVariables();
                    var hostingEnvironment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                    // get environment
                    var env = hostingContext.HostingEnvironment;
                    cfg.AddJsonFile("appSettings.json");
                    cfg.AddJsonFile($"appSettings.{hostingEnvironment}.json", true);
                    var builtConfig = cfg.Build();

                    // Build it so we can use services.
                    var baseUrl = builtConfig.GetValue<string>("BaseConfiguration:BaseUrl");
                    var apikey = builtConfig.GetValue<string>("BaseConfiguration:ApiKey");
                    var section = builtConfig.GetSection(nameof(MatchConfig));
                    mConfiguration = section.Get<MatchConfig>();
                    mConfiguration.SetBaseUrl(baseUrl, apikey);
                })
                .ConfigureServices((context, services) => {
                    services.AddHostedService<Worker>();

                    // Add basic dependency injection services.
                    AddServices(services, mConfiguration, null);
                });

            // Build the host parameters. 
            var host = hostBuilder.Build();
            await host.RunAsync();
        }        

        private static void AddServices(IServiceCollection services, MatchConfig configuration, RunConfiguration runtime)
        {
            services.AddSingleton<IMatchConfiguration>(configuration);
            services.AddSingleton<IMatchInfoRepository, MatchRepository>();
            services.AddSingleton<IPlayerRepository, PlayerRepository>();
            services.AddSingleton<IRunConfiguration>(runtime);
        }
    }
}
