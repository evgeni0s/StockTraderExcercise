using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderExcercise;
using StockTraderModels;
using StockTraderServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitTests.StockTraderServices
{
    /// <summary>
    /// Integration tests, should be in separate project.
    /// </summary>
    [TestClass]
    public class StockTraderServiceTests
    {
        TransactionScope _trans;

        [TestInitialize()]
        public void Init()
        {
            _trans = new TransactionScope();
            AppDomain.CurrentDomain.SetData("DataDirectory", ApplicationFolders.SharedDataDiractory);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _trans.Dispose();
        }
        
        [TestMethod]
        public void AddBondTest()
        {
            var service = new StocksService();
            var stock = new Bond() {
                Price = 50000,
                Quantity = 3
            };
            var newStockId = service.AddStock(stock);
            var newStock = service.GetStock(newStockId);

            Assert.IsNotNull(newStock);
            Assert.AreEqual(stock.Price, newStock.Price);
            Assert.AreEqual(stock.Quantity, newStock.Quantity);
        }
        
        [TestMethod]
        public void AddEquityTest()
        {
            var service = new StocksService();
            var stock = new Equity()
            {
                Price = 50000,
                Quantity = 3
            };
            var newStockId = service.AddStock(stock);
            var newStock = service.GetStock(newStockId);

            Assert.IsNotNull(newStock);
            Assert.AreEqual(stock.Price, newStock.Price);
            Assert.AreEqual(stock.Quantity, newStock.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Sequence contains no elements")]
        public void DeleteBondTest()
        {
            var service = new StocksService();
            var stock = new Bond()
            {
                Price = 50000,
                Quantity = 3
            };
            var newStockId = service.AddStock(stock);
            var newStock = service.GetStock(newStockId);
            Assert.IsNotNull(newStock);
            service.DeleteStock(newStock.Id);
            newStock = service.GetStock(newStockId);
            Assert.IsNull(newStock);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Sequence contains no elements")]
        public void DeleteEquityTest()
        {
            var service = new StocksService();
            var stock = new Equity()
            {
                Price = 50000,
                Quantity = 3
            };
            var newStockId = service.AddStock(stock);
            var newStock = service.GetStock(newStockId);
            Assert.IsNotNull(newStock);
            service.DeleteStock(newStock.Id);
            newStock = service.GetStock(newStockId);
            Assert.IsNull(newStock);
        }

        [TestMethod]
        public void BondNameIsGeneratedTest()
        {
            var service = new StocksService();
            var stock = new Bond()
            {
                Price = 50000,
                Quantity = 3
            };
            var newStockId = service.AddStock(stock);
            var newStock = service.GetStock(newStockId);
            
            Assert.IsTrue(Regex.IsMatch(newStock.Name, @"Bond\d+"));
        }

        [TestMethod]
        public void EquityNameIsGeneratedTest()
        {
            var service = new StocksService();
            var stock = new Equity()
            {
                Price = 50000,
                Quantity = 3
            };
            var newStockId = service.AddStock(stock);
            var newStock = service.GetStock(newStockId);

            Assert.IsTrue(Regex.IsMatch(newStock.Name, @"Equity\d+"));
        }

        [TestMethod]
        public void BondNameIsInctementedTest()
        {
            var service = new StocksService();
            var stock = new Bond()
            {
                Price = 50000,
                Quantity = 3
            };
            var stock1 = new Bond()
            {
                Price = 50000,
                Quantity = 3
            };

            var newStockId = service.AddStock(stock);
            var newStock = service.GetStock(newStockId);
            var newStock1Id = service.AddStock(stock1);
            var newStock1 = service.GetStock(newStock1Id);
            var numericPart1 = Convert.ToInt32(Regex.Match(newStock.Name, @"\d+").Value);
            var numericPart2 = Convert.ToInt32(Regex.Match(newStock1.Name, @"\d+").Value);

            Assert.IsTrue(numericPart2 - numericPart1 == 1);
        }

        [TestMethod]
        public void EquityNameIsInctementedTest()
        {
            var service = new StocksService();
            var stock = new Equity()
            {
                Price = 50000,
                Quantity = 3
            };
            var stock1 = new Equity()
            {
                Price = 50000,
                Quantity = 3
            };

            var newStockId = service.AddStock(stock);
            var newStock = service.GetStock(newStockId);
            var newStock1Id = service.AddStock(stock1);
            var newStock1 = service.GetStock(newStock1Id);
            var numericPart1 = Convert.ToInt32(Regex.Match(newStock.Name, @"\d+").Value);
            var numericPart2 = Convert.ToInt32(Regex.Match(newStock1.Name, @"\d+").Value);

            Assert.IsTrue(numericPart2 - numericPart1 == 1);
        }

        [TestMethod]
        public void GetStocksTest()
        {
            var service = new StocksService();
            var stock = new Equity()
            {
                Price = 50000,
                Quantity = 3
            };
            var stock1 = new Equity()
            {
                Price = 50000,
                Quantity = 3
            };

            var newStockId = service.AddStock(stock);
            var newStock1Id = service.AddStock(stock1);
            var stocks = service.GetStocks();
            Assert.IsTrue(stocks.Any(s => s.Id == newStockId));
            Assert.IsTrue(stocks.Any(s => s.Id == newStock1Id));
        }
    }
}
