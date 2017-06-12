using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderExcercise.Services;
using UnitTests.Mocks;
using System.Collections.Generic;
using StockTraderExcercise.Models;
using StockTraderExcercise.Enums;
using System.Linq;

namespace UnitTests.StockTraderExercise
{
    [TestClass]
    public class StockPropertiesServiceTests
    {
        [TestMethod]
        public void GetStockPropertiesBondsReturnRightTypeTest()
        {
            var service = new StockPropertiesService();
            IList<Stock> bonds = MockHelper.Bonds;
            var properties = service.GetStockProperties(bonds);
            Assert.IsTrue(properties.Count == 1);
            var enmerator = properties.Keys.GetEnumerator();
            enmerator.MoveNext();
            var firstKey = enmerator.Current;
            Assert.IsTrue(firstKey == StockTraderExcercise.Enums.StockType.Bond);
        }

        [TestMethod]
        public void GetStockPropertiesEquityReturnRightTypeTest()
        {
            var service = new StockPropertiesService();
            IList<Stock> bonds = MockHelper.Equities;
            var properties = service.GetStockProperties(bonds);
            Assert.IsTrue(properties.Count == 1);
            var enmerator = properties.Keys.GetEnumerator();
            enmerator.MoveNext();
            var firstKey = enmerator.Current;
            Assert.IsTrue(firstKey == StockTraderExcercise.Enums.StockType.Equity);
        }

        [TestMethod]
        public void GetStockPropertiesEquityReturnDataTest()
        {
            var service = new StockPropertiesService();
            IList<Stock> bonds = MockHelper.Bonds;
            var expectedTotalBonds = new StockProperties();
            expectedTotalBonds.TotalNumber = bonds.Count;
            expectedTotalBonds.TotalMarketValue = bonds.Sum(x => x.MarketValue);

            IList<Stock> equities = MockHelper.Equities;
            var expectedTotalEquities = new StockProperties();
            expectedTotalEquities.TotalNumber = equities.Count;
            expectedTotalEquities.TotalMarketValue = equities.Sum(x => x.MarketValue);

            var totalMarketValue = expectedTotalBonds.TotalMarketValue + expectedTotalEquities.TotalMarketValue;

            expectedTotalBonds.TotalStockWeight = expectedTotalBonds.TotalMarketValue * 100 / totalMarketValue;
            expectedTotalEquities.TotalStockWeight = expectedTotalEquities.TotalMarketValue * 100 / totalMarketValue;

            var stocks = MockHelper.GetStocks();


            var properties = service.GetStockProperties(stocks);
            var actualBond = properties[StockType.Bond];
            var actualEquity = properties[StockType.Equity];
            Assert.AreEqual(actualBond.TotalMarketValue, expectedTotalBonds.TotalMarketValue);
            Assert.AreEqual(actualBond.TotalNumber, expectedTotalBonds.TotalNumber);
            Assert.AreEqual(actualBond.TotalStockWeight, expectedTotalBonds.TotalStockWeight);
            Assert.AreEqual(actualEquity.TotalMarketValue, expectedTotalEquities.TotalMarketValue);
            Assert.AreEqual(actualEquity.TotalNumber, expectedTotalEquities.TotalNumber);
            Assert.AreEqual(actualEquity.TotalStockWeight, expectedTotalEquities.TotalStockWeight);
        }

    }
}
