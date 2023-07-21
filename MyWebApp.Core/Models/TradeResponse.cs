using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Core.Models
{
    public class TradeResponse
    {
        public int CustomerId { get; set; }
        public decimal Stocks { get; set; }
        public decimal Bonds { get; set; }
        public decimal Cash { get; set; }
        public TradeExecutionStatus ExecutionStatus { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public enum TradeExecutionStatus
    {
        Success,
        Failed
    }
}
