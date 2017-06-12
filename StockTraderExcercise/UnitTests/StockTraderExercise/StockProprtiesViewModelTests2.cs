using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
using Rhino.Mocks;
using StockTraderExcercise.Enums;
using StockTraderExcercise.Events;
using StockTraderExcercise.Interfaces;
using StockTraderExcercise.Models;
using StockTraderExcercise.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mocks;

namespace UnitTests.StockTraderExercise
{
    [TestClass]
    public class StockProprtiesViewModelTests2
    {
        IStockPropertiesService stockPropertiesService;
        IEventAggregator eventAggregator;
        [TestInitialize()]
        public void Init()
        {
            MockHelper.ConfigureContainer();
            stockPropertiesService = MockRepository.GenerateMock<IStockPropertiesService>();
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            ServiceLocator.Current.Stub(x => x.GetInstance<IStockPropertiesService>()).Return(stockPropertiesService);
            ServiceLocator.Current.Stub(x => x.GetInstance<IStockProprtiesViewModel>()).Return(new StockProprtiesViewModel(eventAggregator, stockPropertiesService));

        }

        [TestMethod]
        public void StockProprtiesViewModelCanAddsItems()
        {
            var dict = new Dictionary<StockType, StockProperties>();
            var propBond = MockHelper.SimpleProperties;
            var propEquity = MockHelper.SimpleProperties;
            dict.Add(StockType.Bond, propBond);
            dict.Add(StockType.Equity, propEquity);
            stockPropertiesService.Stub(x => x.GetStockProperties(Arg<IList<Stock>>.Is.Anything)).Return(dict);
            var vm = (StockProprtiesViewModel)ServiceLocator.Current.GetInstance<IStockProprtiesViewModel>();
            vm.OnStocksLoaded(new List<Stock>());
            Assert.IsTrue(propBond == vm.BondProperties);
            Assert.IsTrue(propEquity == vm.EquityProperties);
        }
    }
}
