using System;
using System.Threading;
using System.Threading.Tasks;
using Learning.FootballPrediction.DataFetch.Api.Source;

namespace Learning.FootballPrediction.DataFetch
{
    class Program
    {
        private const string URI = "http://localhost:4990/v2/competitions/{0}/Matches?season={1}&matchday={2}";
        public static async Task Main(string[] args)
        {
            var port = -1;
            if(args.Length != 1 || !int.TryParse(args[0], out port))
            {
                Console.Out.WriteLine("Usage: DataFetch [PortNumber]");
                port = 4991; 
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls($"http://*:{port}");
                });
    }
}
