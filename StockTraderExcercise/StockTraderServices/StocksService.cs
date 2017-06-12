using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderServices.Interfaces;
using System.ComponentModel.Composition;
using StockTraderModels;
using Dapper;
using System.Transactions;
using System.Dynamic;

namespace StockTraderServices
{
    [Export(typeof(IStocksService))]
    public class StocksService : IStocksService
    {
        private const string NEXT = "Next";

        public Stock GetStock(int id)
        {
            Stock stock;
            var sql = "select * from Stocks where Id = @id";
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                stock = connection.Query(sql, new { id }).Select(DapperHelpers.ConvertToStock).Single();
            }
            return stock;
        }
        
        public List<Stock> GetStocks()
        {
            List<Stock> stocks;
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                stocks = connection.Query("select * from Stocks").Select(DapperHelpers.ConvertToStock).ToList();
            }
            return stocks;
        }

        public int AddStock(Stock stock)
        {
            var descriminator = stock.GetType().Name;
            var name = GetNextStockName(descriminator);
            var sql = @"insert into Stocks(Name, Price, Quantity, Descriminator)
            values (@Name, @Price, @Quantity, @Descriminator);
            SELECT CAST(SCOPE_IDENTITY() as int)";
            int id = 0;

            using (var ts = new TransactionScope())
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                id = connection.Query<int>(sql, new { name, stock.Price, stock.Quantity, descriminator }).Single();
                ts.Complete();
            }
            return id;
        }


        public void DeleteStock(int id)
        {
            var sql = @"delete from Stocks where Id = @id";

            using (var ts = new TransactionScope())
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                connection.Execute(sql, new { id });
                ts.Complete();
            }
        }

        private string GetNextStockName(string descriminator) => descriminator + (BookNextStockNames(descriminator, 1) - 1);

        private int BookNextStockNames(string descriminator, int count)
        {
            int nextId = GetNextStockId(descriminator);
            var seedKey = NEXT + descriminator;
            var newNext = nextId + count;
            var sql = $@"update TOP (1) IdSeeds set {seedKey} = @newNext";

            using (var ts = new TransactionScope())
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                connection.Execute(sql, new { newNext });
                ts.Complete();
            }
            return newNext;
        }

        private int GetNextStockId(string descriminator)
        {
            int nextId;
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var seedKey = NEXT + descriminator;
                var sql = $@"select TOP (1) {seedKey} from IdSeeds";
                nextId = connection.Query<int>(sql).Single();
            }
            return nextId;
        }
    }
}
