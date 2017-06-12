using StockTraderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderServices.Interfaces
{
    public interface IStocksService
    {
        Stock GetStock(int id);
        List<Stock> GetStocks();
        int AddStock(Stock stock);
        void DeleteStock(int id);
    }
}
