using StockTraderExcercise.Enums;
using StockTraderExcercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderExcercise.Interfaces
{
    public interface IStockPropertiesService
    {
        Dictionary<StockType, StockProperties> GetStockProperties(IList<Stock> stocks);
    }
}
