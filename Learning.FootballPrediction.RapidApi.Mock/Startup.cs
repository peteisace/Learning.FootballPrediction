using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.FootballPrediction.RapidApi.Mock.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Learning.FootballPrediction.RapidApi.Mock
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Learning.FootballPrediction.RapidApi.Mock", Version = "v1" });
            });

            AddServices(services);
        }

        private void AddServices(IServiceCollection services)
        {
            var section = this.Configuration.GetSection(nameof(UrlConfiguration));
            if(section != null)
            {
                var config = section.Get<UrlConfiguration>();
                services.AddSingleton<IUrlConfiguration>(config);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learning.FootballPrediction.RapidApi.Mock v1"));
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.AddTokenAuthenticationMiddleware();
            app.UseJsonContentByUrl();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
