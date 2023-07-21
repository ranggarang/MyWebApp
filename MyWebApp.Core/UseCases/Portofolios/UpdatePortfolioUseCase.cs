using MyWebApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebApp.Core.UseCases.Portofolios
{
    public class UpdatePortfolioUseCase
    {
        private readonly IExternalPortfolioService _externalPortfolioService;

        public UpdatePortfolioUseCase(IExternalPortfolioService externalPortfolioService)
        {
            _externalPortfolioService = externalPortfolioService;
        }

        public void Execute(int customerId, decimal stocksPercentage, decimal bondsPercentage, decimal cashPercentage)
        {
            // Update the customer's portfolio in the external service
            var customers = _externalPortfolioService.GetCustomers();
            var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                // Customer not found, handle error or create a new customer portfolio
            }

            // Update the customer's portfolio percentages
            customer.Stocks = stocksPercentage;
            customer.Bonds = bondsPercentage;
            customer.Cash = cashPercentage;

            // Save the updated portfolio back to the external service
            _externalPortfolioService.UpdateCustomerDataAsync(customers);
        }
    }
}
