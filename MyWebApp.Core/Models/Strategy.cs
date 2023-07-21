using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Models
{
    public class Strategy
    {
        public int StrategyId { get; set; }
        public int MinRiskLevel { get; set; }
        public int MaxRiskLevel { get; set; }
        public int MinYearsToRetirement { get; set; }
        public int MaxYearsToRetirement { get; set; }
        public decimal StocksPercentage { get; set; }
        public decimal CashPercentage { get; set; }
        public decimal BondsPercentage { get; set; }
    }
}
