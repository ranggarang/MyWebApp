using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Core.Models
{
    public class PortfolioUpdateRequest
    {
        public int CustomerId { get; set; }
        public decimal StocksPercentage { get; set; }
        public decimal BondsPercentage { get; set; }
        public decimal CashPercentage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
