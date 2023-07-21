using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebApp.Core.UseCases.Portofolios
{
    public class GetPortfolioUseCase
    {
        private readonly IExternalPortfolioService _externalPortfolioService;

        public GetPortfolioUseCase(IExternalPortfolioService externalPortfolioService)
        {
            _externalPortfolioService = externalPortfolioService;
        }

        public Portfolio Execute(int customerId)
        {
            // Retrieve the customer portfolio from the external service
            var customers = _externalPortfolioService.GetCustomers();
            var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return null; // Customer not found
            }

            // Return the customer's portfolio
            return new Portfolio
            {
                CustomerId = customer.CustomerId,
                Stocks = customer.Stocks,
                Bonds = customer.Bonds,
                Cash = customer.Cash,
            };
        }
    }
}
