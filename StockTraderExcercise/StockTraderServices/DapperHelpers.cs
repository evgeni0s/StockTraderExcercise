using StockTraderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderServices
{
    public static class DapperHelpers
    {
        public static Stock ConvertToStock(object value)
        {
            var dapperRowProperties = value as IDictionary<string, object>;
            switch (dapperRowProperties["Descriminator"])
            {
                case "Bond":
                    return GetObject<Bond>(dapperRowProperties);
                case "Equity":
                    return GetObject<Equity>(dapperRowProperties);
                default:
                    return null;
            }
        }

        public static T GetObject<T>(IDictionary<string, object> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }
    }
}
