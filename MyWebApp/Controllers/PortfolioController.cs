using Microsoft.AspNetCore.Mvc;
using MyWebApp.Core.Models;
using MyWebApp.Core.UseCases.Portofolios;

namespace MyWebApp.API.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly GetPortfolioUseCase _getPortfolioUseCase;
        private readonly UpdatePortfolioUseCase _updatePortfolioUseCase;

        public PortfolioController(GetPortfolioUseCase getPortfolioUseCase, UpdatePortfolioUseCase updatePortfolioUseCase)
        {
            _getPortfolioUseCase = getPortfolioUseCase;
            _updatePortfolioUseCase = updatePortfolioUseCase;
        }

        [HttpGet("{customerId}")]
        public IActionResult GetPortfolio(int customerId)
        {
            var portfolio = _getPortfolioUseCase.Execute(customerId);

            if (portfolio == null)
            {
                return NotFound();
            }

            return Ok(portfolio);
        }

        [HttpPut("{customerId}")]
        public IActionResult UpdatePortfolio(int customerId, [FromBody] PortfolioUpdateRequest request)
        {
            _updatePortfolioUseCase.Execute(customerId, request.StocksPercentage, request.BondsPercentage, request.CashPercentage);
            return NoContent();
        }
    }
}
