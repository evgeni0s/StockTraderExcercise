using Prism.Events;
using StockTraderExcercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderExcercise.Events
{
    public class StocksLoaded: PubSubEvent<IList<Stock>>
    {
    }
}
