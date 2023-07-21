using Microsoft.AspNetCore.Mvc;
using MyWebApp.Core.Interfaces;
using MyWebApp.Core.UseCases.Trades;

namespace MyWebApp.API.Controllers
{
    [Route("api/trade")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly CalculateTradesUseCase _calculateTradesUseCase;
        private readonly IFinancialPortfolioService _financialPortfolioService;

        public TradeController(CalculateTradesUseCase calculateTradesUseCase, IFinancialPortfolioService financialPortfolioService)
        {
            _calculateTradesUseCase = calculateTradesUseCase;
            _financialPortfolioService = financialPortfolioService;
        }

        [HttpGet("rebalance")]
        public IActionResult GetRebalanceTrades()
        {
            var tradeRequests = _calculateTradesUseCase.Execute();

            if (tradeRequests == null || tradeRequests.Count == 0)
            {
                return NotFound();
            }

            // In a real-world scenario, you would limit the number of trades in each batch based on the configurable setting.
            // For this example, we'll use all the calculated trades in a single batch.
            var tradeResponses = _financialPortfolioService.ExecuteTrades(tradeRequests);

            return Created("api/trade/rebalance", tradeResponses);
        }
    }
}
