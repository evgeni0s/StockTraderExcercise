using AutoMapper;
using StockTraderExcercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalDTOs = StockTraderModels;

namespace StockTraderExcercise.Helpers
{
    static class StocksConvertor
    {
        public static Bond Convert(ExternalDTOs.Bond serviceDto)
        {
            return Mapper.Map<Bond>(serviceDto);
        }
        public static Equity Convert(ExternalDTOs.Equity serviceDto)
        {
            return Mapper.Map<Equity>(serviceDto);
        }
        public static Stock Convert(ExternalDTOs.Stock serviceDto)
        {
            return Convert(serviceDto as dynamic);
        }
    }
}
