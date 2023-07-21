using Microsoft.Extensions.Hosting;
using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using MyWebApp.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebApp.Core.UseCases.Trades
{
    public class DailyCsvConsumptionService : BackgroundService
    {
        private readonly string _dataFolderPath;
        private readonly IExternalPortfolioService _externalPortfolioService;
        private readonly CsvFileService _csvFileService;

        public DailyCsvConsumptionService(string dataFolderPath, IExternalPortfolioService externalPortfolioService)
        {
            _dataFolderPath = dataFolderPath;
            _externalPortfolioService = externalPortfolioService;
            _csvFileService = new CsvFileService(_dataFolderPath);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Read and update customer data
                var customers = _csvFileService.ReadCustomersFromCsv();
                await _externalPortfolioService.UpdateCustomerDataAsync(customers);

                // Read and update strategy data
                var strategies = _csvFileService.ReadStrategiesFromCsv();
                await _externalPortfolioService.UpdateStrategyDataAsync(strategies);

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
