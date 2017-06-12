using StockTraderExcercise.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderExcercise.Enums;
using StockTraderExcercise.Models;

namespace StockTraderExcercise.Services
{
    [Export(typeof(IStockPropertiesService))]
    public class StockPropertiesService : IStockPropertiesService
    {
        public Dictionary<StockType, StockProperties> GetStockProperties(IList<Stock> stocks)
        {
            var propertiesDictionaty = new Dictionary<StockType, StockProperties>();
            // optimisation. Smaller subset of items
            //var stocksMatchingType = new List<Stock>();
            decimal TotalMarketValue = 0;
            foreach (var stock in stocks)
            {
                if (!propertiesDictionaty.ContainsKey(stock.StockType))
                {
                    propertiesDictionaty.Add(stock.StockType, new StockProperties());
                }
                propertiesDictionaty[stock.StockType].TotalMarketValue += stock.MarketValue;
                propertiesDictionaty[stock.StockType].TotalNumber++;
                TotalMarketValue = stock.TotalMarketValue;
            }
            foreach (var propItem in propertiesDictionaty.Values)
            {
                propItem.TotalStockWeight = propItem.TotalMarketValue * 100 / TotalMarketValue;
            }
            return propertiesDictionaty;
        }
    }
}
