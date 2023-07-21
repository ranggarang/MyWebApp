using MyWebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Core.Interfaces
{
    public interface IExternalPortfolioService
    {
        List<Customer> GetCustomers();
        List<Strategy> GetStrategies();
        Task<List<Customer>> UpdateCustomerDataAsync(List<Customer> customers);
        Task<List<Strategy>> UpdateStrategyDataAsync(List<Strategy> strategies);
    }
}
