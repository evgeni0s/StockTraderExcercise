using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderModels
{
    public abstract class Stock
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string Name { get; set; }
        public string Descriminator { get; set; }
    }
}
