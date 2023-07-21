using Moq;
using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using MyWebApp.Core.UseCases.Portofolios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyWebApp.Core.Tests.UseCases.Portofolios
{
    public class GetPortfolioUseCaseTests
    {
        [Fact]
        public void Execute_WithExistingCustomer_ReturnsCorrectPortfolio()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer { CustomerId = customerId, Stocks = 50, Bonds = 30, Cash = 20 };

            var mockExternalPortfolioService = new Mock<IExternalPortfolioService>();
            mockExternalPortfolioService.Setup(s => s.GetCustomers()).Returns(new List<Customer> { customer });

            var getPortfolioUseCase = new GetPortfolioUseCase(mockExternalPortfolioService.Object);

            // Act
            var result = getPortfolioUseCase.Execute(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
            Assert.Equal(customer.Stocks, result.Stocks);
            Assert.Equal(customer.Bonds, result.Bonds);
            Assert.Equal(customer.Cash, result.Cash);
        }

        [Fact]
        public void Execute_WithNonExistingCustomer_ReturnsNull()
        {
            // Arrange
            var customerId = 2;
            var customer = new Customer { CustomerId = 1, Stocks = 50, Bonds = 30, Cash = 20 };

            var mockExternalPortfolioService = new Mock<IExternalPortfolioService>();
            mockExternalPortfolioService.Setup(s => s.GetCustomers()).Returns(new List<Customer> { customer });

            var getPortfolioUseCase = new GetPortfolioUseCase(mockExternalPortfolioService.Object);

            // Act
            var result = getPortfolioUseCase.Execute(customerId);

            // Assert
            Assert.Null(result);
        }
    }
}
