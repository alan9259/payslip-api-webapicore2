using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PaySlip
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PayPeriod { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Super { get; set; }
    }
}
