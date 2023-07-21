using Moq;
using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using MyWebApp.Core.UseCases.Trades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyWebApp.Core.Tests.UseCases.Trades
{
    public class CalculateTradesUseCaseTests
    {
        [Fact]
        public void Execute_WithMatchingStrategy_CalculatesTradeRequest()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, RiskLevel = 3, RetirementAge = 65, Stocks = 50, Bonds = 30, Cash = 20 };
            var strategy = new Strategy { StrategyId = 1, MinRiskLevel = 1, MaxRiskLevel = 5, MinYearsToRetirement = 55, MaxYearsToRetirement = 75, StocksPercentage = 40, BondsPercentage = 30, CashPercentage = 30 };
            var mockExternalPortfolioService = new Mock<IExternalPortfolioService>();
            mockExternalPortfolioService.Setup(s => s.GetCustomers()).Returns(new List<Customer> { customer });
            mockExternalPortfolioService.Setup(s => s.GetStrategies()).Returns(new List<Strategy> { strategy });

            var calculateTradesUseCase = new CalculateTradesUseCase(mockExternalPortfolioService.Object);

            // Act
            var result = calculateTradesUseCase.Execute();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            var tradeRequest = result.First();
            Assert.Equal(customer.CustomerId, tradeRequest.CustomerId);
            Assert.Equal(strategy.StocksPercentage - customer.Stocks, tradeRequest.StocksPercentage);
            Assert.Equal(strategy.BondsPercentage - customer.Bonds, tradeRequest.BondsPercentage);
            Assert.Equal(strategy.CashPercentage - customer.Cash, tradeRequest.CashPercentage);
        }

        [Fact]
        public void Execute_WithNoMatchingStrategy_CalculatesAllCashTradeRequest()
        {
            // Arrange
            var customer = new Customer { CustomerId = 2, RiskLevel = 10, RetirementAge = 45, Stocks = 60, Bonds = 20, Cash = 20 };
            var strategies = new List<Strategy>
            {
                new Strategy { StrategyId = 1, MinRiskLevel = 1, MaxRiskLevel = 5, MinYearsToRetirement = 55, MaxYearsToRetirement = 75, StocksPercentage = 40, BondsPercentage = 30, CashPercentage = 30 },
                new Strategy { StrategyId = 2, MinRiskLevel = 6, MaxRiskLevel = 9, MinYearsToRetirement = 55, MaxYearsToRetirement = 75, StocksPercentage = 20, BondsPercentage = 10, CashPercentage = 70 }
            };
            var mockExternalPortfolioService = new Mock<IExternalPortfolioService>();
            mockExternalPortfolioService.Setup(s => s.GetCustomers()).Returns(new List<Customer> { customer });
            mockExternalPortfolioService.Setup(s => s.GetStrategies()).Returns(strategies);

            var calculateTradesUseCase = new CalculateTradesUseCase(mockExternalPortfolioService.Object);

            // Act
            var result = calculateTradesUseCase.Execute();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            var tradeRequest = result.First();
            Assert.Equal(customer.CustomerId, tradeRequest.CustomerId);
            Assert.Equal(-customer.Stocks, tradeRequest.StocksPercentage);
            Assert.Equal(-customer.Bonds, tradeRequest.BondsPercentage);
            Assert.Equal(100 - customer.Cash, tradeRequest.CashPercentage);
        }
    }
}
