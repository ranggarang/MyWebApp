using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Infrastructure
{
    public class ExternalPortfolioService : IExternalPortfolioService
    {
        private readonly string _dataFolderPath;
        private List<Customer> _customers;
        private List<Strategy> _strategies;

        public ExternalPortfolioService(string dataFolderPath)
        {
            _dataFolderPath = dataFolderPath;
            _customers = new List<Customer>();
            _strategies = new List<Strategy>();
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public List<Strategy> GetStrategies()
        {
            return _strategies;
        }

        public async Task<List<Customer>> UpdateCustomerDataAsync(List<Customer> customers)
        {
            _customers = customers;
            return _customers;
        }

        public async Task<List<Strategy>> UpdateStrategyDataAsync(List<Strategy> strategies)
        {
            _strategies = strategies;
            return _strategies;
        }
    }
}
