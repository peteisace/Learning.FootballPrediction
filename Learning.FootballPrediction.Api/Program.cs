using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Learning.FootballPrediction.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            var port = 4991;         
            if(args.Length >= 1)
            {
                port = int.Parse(args[0]);
            }

            CreateHostBuilder(args, port).Build().Run();
        }        

        public static IHostBuilder CreateHostBuilder(string[] args, int port) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, cfg) => {
                    cfg.AddEnvironmentVariables();
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    cfg.AddJsonFile("appsettings.json");
                    cfg.AddJsonFile($"appsettings.{env}.json", optional: true);
                    cfg.Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {                
                    webBuilder.UseWebRoot("wwwroot");   
                    webBuilder.UseStartup<Startup>()
                        .UseUrls($"http://*:{port}");
                });
    }
}
