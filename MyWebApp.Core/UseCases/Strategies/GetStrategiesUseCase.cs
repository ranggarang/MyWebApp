using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.UseCases.Strategies
{
    public class GetStrategiesUseCase
    {
        private readonly IExternalPortfolioService _externalPortfolioService;

        public GetStrategiesUseCase(IExternalPortfolioService externalPortfolioService)
        {
            _externalPortfolioService = externalPortfolioService;
        }

        public List<Strategy> Execute()
        {
            // Retrieve the list of strategies from the external service
            return _externalPortfolioService.GetStrategies();
        }
    }
}
