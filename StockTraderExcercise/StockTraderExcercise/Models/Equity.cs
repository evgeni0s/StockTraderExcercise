using StockTraderExcercise.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderExcercise.Models
{
    public class Equity : Stock
    {
        private const int TOLERANCE = 200000;
        public override decimal TransactionCost => MarketValue / 100 * 0.5m;
        public override bool IsHighlighted => base.IsHighlighted || TransactionCost > TOLERANCE;
        public override StockType StockType => StockType.Equity;
    }
}
