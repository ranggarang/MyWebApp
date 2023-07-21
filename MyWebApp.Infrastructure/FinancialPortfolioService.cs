using Microsoft.Extensions.Options;
using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Infrastructure
{
    public class FinancialPortfolioService : IFinancialPortfolioService
    {
        private DateTime _lastProcessedTimestamp = DateTime.MinValue; // Initialize to the minimum value
        private readonly AppSettings _appSettings;
        private readonly IExternalPortfolioService _externalPortfolioService;

        public FinancialPortfolioService(IExternalPortfolioService externalPortfolioService, IOptions<AppSettings> appSettings)
        {
            _externalPortfolioService = externalPortfolioService;
            _appSettings = appSettings.Value;
        }

        public List<TradeResponse> ExecuteTrades(List<TradeRequest> tradeRequests)
        {
            // Filter out trades with timestamps before the last processed timestamp
            tradeRequests = (List<TradeRequest>)tradeRequests.Where(tradeRequest => tradeRequest.Timestamp > _lastProcessedTimestamp);

            // Limit the number of trades per batch
            tradeRequests = (List<TradeRequest>)tradeRequests.Take(_appSettings.MaxTradesPerBatch);

            // ...

            // Simulate the execution of trades and return some dummy trade responses
            var tradeResponses = new List<TradeResponse>();

            foreach (var tradeRequest in tradeRequests)
            {
                
                // Simulate the execution of trades and return some dummy trade responses
                var tradeResponse = new TradeResponse
                {
                    CustomerId = tradeRequest.CustomerId,
                    Stocks = tradeRequest.StocksPercentage,
                    Bonds = tradeRequest.BondsPercentage,
                    Cash = tradeRequest.CashPercentage,
                    ExecutionStatus = TradeExecutionStatus.Success,
                    Timestamp = DateTime.Now
                };


                // Update the last processed timestamp to the current trade's timestamp
                _lastProcessedTimestamp = tradeRequest.Timestamp;

                tradeResponses.Add(tradeResponse);
            }

            return tradeResponses;
        }

       
    }
}

