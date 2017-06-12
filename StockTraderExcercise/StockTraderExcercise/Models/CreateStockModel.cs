
using StockTraderExcercise.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderExcercise.Models
{
    public class CreateStockModel //: IValidatableObject
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string PriceString { get; set; }
        public string QuantityString { get; set; }
        public StockType StockType { get; internal set; }

    }
}
