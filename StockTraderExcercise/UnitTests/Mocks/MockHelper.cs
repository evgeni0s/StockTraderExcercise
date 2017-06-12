using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Rhino.Mocks;
using StockTraderExcercise.Events;
using StockTraderExcercise.Models;
//using StockTraderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    public static class MockHelper
    {
        public static StockProperties SimpleProperties => new StockProperties
        {
            TotalMarketValue = 123,
            TotalNumber = 12,
            TotalStockWeight = 1234
        };

        public static Equity SimpleEquity => new Equity
        {
            Price = 1000,
            Quantity = 5
        };

        public static IList<Equity> GetEquties()
        {
            var list  = new List<Equity> {
            new Equity{ Price = 1000, Quantity = 5 },
            new Equity{ Price = 10000, Quantity = 2 },
            new Equity{ Price = 3333, Quantity = 1 },
            new Equity{ Price = 100, Quantity = 50 },
            };

            SetTotalMarketValue(list);
            return list;
        }

        public static IList<Stock> Equities => GetEquties().Select(x => (Stock)x).ToList();

        public static IList<Bond> GetBonds()
        {
            var list = new List<Bond>() {
            new Bond{ Price = 2000, Quantity = 5 },
            new Bond{ Price = 30000, Quantity = 2 },
            new Bond{ Price = 77777, Quantity = 1 },
            new Bond{ Price = 999, Quantity = 50 },
            };
            SetTotalMarketValue(list);
            return list;
        }

        public static IList<Stock> Bonds => GetBonds().Select(x => (Stock)x).ToList();

        private static void SetTotalMarketValue<T>(IList<T> list) where T: Stock
        {
            var totalMarketValue = list.Sum(x => x.Price * x.Quantity);
            foreach (var item in list)
            {
                item.SetTotalMarketValue(totalMarketValue);
            }
        }

        public static IList<Stock> GetStocks()
        {
            var list = new List<Stock>();
            list.AddRange(GetEquties());
            list.AddRange(GetBonds());
            SetTotalMarketValue(list);
            return list;
        }



        public static void ConfigureContainer()
        {
            var eventAggregator = MockRepository.GenerateMock<IEventAggregator>();
            eventAggregator.Stub(x => x.GetEvent<NewStockAdded>()).Return(new NewStockAdded());
            eventAggregator.Stub(x => x.GetEvent<StocksLoaded>()).Return(new StocksLoaded());
            var serviceLocator = MockRepository.GenerateMock<IServiceLocator>();
            serviceLocator.Stub(x => x.GetInstance<IEventAggregator>()).Return(eventAggregator);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            //return serviceLocator;
        }
    }
}
