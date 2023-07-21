using MyWebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Core.Services
{
    public class CsvFileService
    {
        private readonly string _dataFolderPath;

        public CsvFileService(string dataFolderPath)
        {
            _dataFolderPath = dataFolderPath;
        }

        public List<Customer> ReadCustomersFromCsv()
        {
            var filePath = Path.Combine(_dataFolderPath, "customer.csv");
            // Implement the logic to read and parse customer data from the CSV file into a list of Customer objects
            // Sample implementation (you may need to customize this depending on your CSV format):
            var csvLines = File.ReadAllLines(filePath).Skip(1); // Skip the header row
            var customers = csvLines.Select(line =>
            {
                var values = line.Split(',');
                return new Customer
                {
                    CustomerId = int.Parse(values[0]),
                    Email = values[1],
                    DateOfBirth = DateTime.Parse(values[2]),
                    RiskLevel = int.Parse(values[3]),
                    RetirementAge = int.Parse(values[4]),
                    Stocks = decimal.Parse(values[5]),
                    Bonds = decimal.Parse(values[6]),
                    Cash = decimal.Parse(values[7])
                };
            }).ToList();

            return customers;
        }

        public List<Strategy> ReadStrategiesFromCsv()
        {
            var filePath = Path.Combine(_dataFolderPath, "strategies.csv");
            // Implement the logic to read and parse strategy data from the CSV file into a list of Strategy objects
            // Sample implementation (you may need to customize this depending on your CSV format):
            var csvLines = File.ReadAllLines(filePath).Skip(1); // Skip the header row
            var strategies = csvLines.Select(line =>
            {
                var values = line.Split(',');
                return new Strategy
                {
                    StrategyId = int.Parse(values[0]),
                    MinRiskLevel = int.Parse(values[1]),
                    MaxRiskLevel = int.Parse(values[2]),
                    MinYearsToRetirement = int.Parse(values[3]),
                    MaxYearsToRetirement = int.Parse(values[4]),
                    StocksPercentage = decimal.Parse(values[5]),
                    BondsPercentage = decimal.Parse(values[6]),
                    CashPercentage = decimal.Parse(values[7])
                };
            }).ToList();

            return strategies;
        }
    }
}
