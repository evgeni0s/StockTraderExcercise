using StockTraderExcercise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderExcercise.Enums;
using StockTraderExcercise.Models;
using StockTraderModels;

namespace UnitTests.Mocks
{
    public class StockPropertiesServiceMock : IStockPropertiesService
    {
        public Dictionary<StockType, StockProperties> GetStockProperties(IList<StockTraderExcercise.Models.Stock> stocks)
        {
            throw new NotImplementedException();
        }
    }
}
