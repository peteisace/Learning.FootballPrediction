

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
        /// <param name="noOfMatchdays">An option whose value states the number of matches for this EPL season</param>
        /// <param name="startYear">An option whose value states the starting season year that we look at matches</param>
        public static async Task Main(int noOfMatchdays = 38, int startYear = 2018)
        {
            MatchConfig mConfiguration = null;
            RunConfiguration rConfiguration = null;

            var hostBuilder = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(configBuilder => {
                    configBuilder.AddEnvironmentVariables();
                    rConfiguration = new RunConfiguration(noOfMatchdays, startYear);
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
                    var dUrl = builtConfig.GetValue<string>("BaseConfiguration:FpApi");
                    var section = builtConfig.GetSection(nameof(MatchConfig));
                    mConfiguration = section.Get<MatchConfig>();
                    mConfiguration.SetBaseUrl(baseUrl, apikey);
                    mConfiguration.SetDestination(dUrl);
                })
                .ConfigureServices((context, services) => {
                    services.AddHostedService<Worker>();

                    // Add basic dependency injection services.
                    AddServices(services, mConfiguration, rConfiguration);
                });

            // Build the host parameters. 
            var host = hostBuilder.Build();
            await host.RunAsync();
        }
        /*
        public static async Task Main(string[] args)
        {
            
        }      
        */  

        private static void AddServices(IServiceCollection services, MatchConfig configuration, RunConfiguration runtime)
        {
            services.AddSingleton<IMatchConfiguration>(configuration);
            services.AddSingleton<IMatchInfoRepository, MatchRepository>();
            services.AddSingleton<IPlayerRepository, PlayerRepository>();
            services.AddSingleton<IRunConfiguration>(runtime);
        }
    }
}
