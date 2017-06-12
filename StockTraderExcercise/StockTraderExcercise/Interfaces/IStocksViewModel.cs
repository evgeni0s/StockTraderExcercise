using StockTraderExcercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderExcercise.Interfaces
{
    interface IStocksViewModel
    {
        void Initialize(StocksModel model);
        void Refresh();
    }
}
