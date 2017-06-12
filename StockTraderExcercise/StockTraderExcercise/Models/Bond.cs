using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderExcercise.Enums;

namespace StockTraderExcercise.Models
{
    public class Bond : Stock
    {
        private const int TOLERANCE = 100000;
        public override decimal TransactionCost => MarketValue / 100 * 2.0m;
        public override bool IsHighlighted => base.IsHighlighted || TransactionCost > TOLERANCE;

        public override StockType StockType => StockType.Bond;
    }
}
