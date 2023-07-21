using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RiskLevel { get; set; }
        public int RetirementAge { get; set; }
        public decimal Stocks { get; set; }
        public decimal Bonds { get; set; }
        public decimal Cash { get; set; }
    }
}
