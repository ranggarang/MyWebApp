using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebApp.Core.UseCases.Trades
{
    public class CalculateTradesUseCase
    {
        private readonly IExternalPortfolioService _externalPortfolioService;

        public CalculateTradesUseCase(IExternalPortfolioService externalPortfolioService)
        {
            _externalPortfolioService = externalPortfolioService;
        }

        public List<TradeRequest> Execute()
        {
            var tradeRequests = new List<TradeRequest>();
            var customers = _externalPortfolioService.GetCustomers();
            var strategies = _externalPortfolioService.GetStrategies();

            foreach (var customer in customers)
            {
                var matchingStrategy = GetMatchingStrategy(customer, strategies);

                if (matchingStrategy != null)
                {
                    var tradeRequest = CalculateTradeRequest(customer, matchingStrategy);
                    tradeRequests.Add(tradeRequest);
                }
                else
                {
                    var tradeRequest = CalculateAllCashTradeRequest(customer);
                    tradeRequests.Add(tradeRequest);
                }
            }

            return tradeRequests;
        }

        private Strategy GetMatchingStrategy(Customer customer, List<Strategy> strategies)
        {
            return strategies.FirstOrDefault(strategy =>
                customer.RiskLevel >= strategy.MinRiskLevel &&
                customer.RiskLevel <= strategy.MaxRiskLevel &&
                customer.RetirementAge >= strategy.MinYearsToRetirement &&
                customer.RetirementAge <= strategy.MaxYearsToRetirement);
        }

        private TradeRequest CalculateTradeRequest(Customer customer, Strategy strategy)
        {
            var tradeRequest = new TradeRequest
            {
                CustomerId = customer.CustomerId,
                StocksPercentage = strategy.StocksPercentage - customer.Stocks,
                BondsPercentage = strategy.BondsPercentage - customer.Bonds,
                CashPercentage = strategy.CashPercentage - customer.Cash
            };

            return tradeRequest;
        }

        private TradeRequest CalculateAllCashTradeRequest(Customer customer)
        {
            var tradeRequest = new TradeRequest
            {
                CustomerId = customer.CustomerId,
                StocksPercentage = -customer.Stocks,
                BondsPercentage = -customer.Bonds,
                CashPercentage = 100 - customer.Cash
            };

            return tradeRequest;
        }
    }
}
