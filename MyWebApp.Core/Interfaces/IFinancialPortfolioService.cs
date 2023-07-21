using MyWebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Interfaces
{
    public interface IFinancialPortfolioService
    {
        List<TradeResponse> ExecuteTrades(List<TradeRequest> tradeRequests);
    }
}
