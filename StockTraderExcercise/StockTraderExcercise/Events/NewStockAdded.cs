using Prism.Events;
using StockTraderExcercise.Models;
using System;
using System.Collections.Generic;

namespace StockTraderExcercise.Events
{
    public class NewStockAdded : PubSubEvent<int>
    {
    }
}
