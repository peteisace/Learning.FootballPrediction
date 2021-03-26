using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Learning.FootballPrediction.RapidApi.Mock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, cfg) => {
                    cfg.AddEnvironmentVariables();
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    cfg.AddJsonFile("appsettings.json");
                    cfg.AddJsonFile($"appsettings.{env}.json");
                    cfg.Build();
                })
                .ConfigureServices((host, services) => {
                    
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://*:4992");
                });
    }
}
