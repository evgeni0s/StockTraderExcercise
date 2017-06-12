using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderExcercise.Models
{
    class StocksModel
    {   
        public IList<Stock> Stocks { get; set; }
        public decimal TotalMarketValue { get; internal set; }
    }
}
