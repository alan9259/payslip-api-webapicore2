using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public struct Tax
    {
        public readonly decimal Low;
        public readonly decimal High;
        public readonly decimal TaxPerDollar;
        public readonly decimal CarryOver;

        public Tax(
            decimal low,
            decimal high,
            decimal taxPerDollar,
            decimal carryOver)
        {
            Low = low;
            High = high;
            TaxPerDollar = taxPerDollar;
            CarryOver = carryOver;
        }
    }
}
