using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Learning.FootballPrediction.ApiMock
{
    public class Program
    {
        private static int port = -1;

        public static void Main(string[] args)
        {
            if(args.Length != 1 || !int.TryParse(args[0], out port))
            {
                Console.Out.WriteLine("Usage: ApiMock [PortNumber]");
                port = 4990; 
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
