using StockTraderExcercise.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderExcercise.Models
{
    public abstract class Stock
    {
        private decimal totalMarketValue;
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string Name { get; set; }
        public decimal MarketValue => Price * Quantity;
        public abstract decimal TransactionCost { get; }
        public abstract StockType StockType { get; }
        public virtual bool IsHighlighted => MarketValue < 0;
        public decimal StockWeight => MarketValue * 100 / totalMarketValue;
        public decimal TotalMarketValue => totalMarketValue;

        public void SetTotalMarketValue(decimal value)
        {
            totalMarketValue = value;
        }
    }
}
