using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using MyWebApp.Core.UseCases.Portofolios;
using MyWebApp.Core.UseCases.Strategies;
using MyWebApp.Core.UseCases.Trades;
using MyWebApp.Infrastructure;

namespace MyWebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IExternalPortfolioService, ExternalPortfolioService>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var dataFolderPath = Configuration.GetValue<string>("DataFolderPath");
            services.AddScoped<IFinancialPortfolioService, FinancialPortfolioService>();
            services.AddSingleton(new DailyCsvConsumptionService(dataFolderPath, services.BuildServiceProvider().GetService<IExternalPortfolioService>()));

            services.AddScoped<GetPortfolioUseCase>();
            services.AddScoped<UpdatePortfolioUseCase>();
            services.AddScoped<GetStrategiesUseCase>();
            services.AddScoped<CalculateTradesUseCase>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
