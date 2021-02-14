using home_dashboard_api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace home_dashboard_api
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
            var appSettings = Configuration.Get<AppSettings>();

            services
                .AddSwaggerGenNewtonsoftSupport()
                .AddMvcCore()
                .AddApiExplorer()
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Home Dashboard API", Version = "v1" });
            });            

            services.AddHttpClient("coinmarketcap", c =>
            {
                c.BaseAddress = new Uri(appSettings.CoinmarketcapSettings.BaseUrl);
                c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", appSettings.CoinmarketcapSettings.ApiKey);
                c.DefaultRequestHeaders.Add("Accepts", "application/json");
                c.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip");
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

            services.AddHttpClient("landfill", c =>
            {
                c.BaseAddress = new Uri(appSettings.LandfillSettings.BaseUrl);
                c.DefaultRequestHeaders.Add("Accepts", "application/json");
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

            services.AddSingleton<IAppSettings>(appSettings);
            services.AddScoped<ICoinmarketcapService, CoinmarketcapService>();
            services.AddScoped<ILandfillService, LandfillService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Home Dashboard API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
