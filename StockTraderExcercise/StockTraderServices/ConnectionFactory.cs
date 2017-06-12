using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderServices
{
    public class ConnectionFactory
    {
        public static DbConnection GetOpenConnection()
        {

            /*this proj does not have .config file, so connection string provided by StockTraderExercise -> App.config*/
            var connectionString = ConfigurationManager.ConnectionStrings["StockTraderConnectionString"];
            var connection = new SqlConnection(connectionString.ConnectionString);
            connection.Open();

            return connection;
        }
    }
}
