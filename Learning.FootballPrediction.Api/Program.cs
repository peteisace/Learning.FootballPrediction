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

            CreateHostBuilder(args).Build().Run();
        }        

        public static IHostBuilder CreateHostBuilder(string[] args, int port) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseWebRoot("wwwroot");   
                    webBuilder.UseStartup<Startup>()
                        .UseUrls($"http://*:{port}");
                });
    }
}
