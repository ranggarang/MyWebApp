using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Models
{
    public class Portfolio
    {
        public int CustomerId { get; set; }
        public decimal Stocks { get; set; }
        public decimal Bonds { get; set; }
        public decimal Cash { get; set; }
    }
}
