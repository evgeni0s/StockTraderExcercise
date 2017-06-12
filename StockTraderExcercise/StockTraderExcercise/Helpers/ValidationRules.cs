using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderExcercise.Helpers
{
    static class ValidationRules
    {
        public static bool IsDecimal(object value)
        {
            decimal i;
            var isSuccess = value != null && decimal.TryParse(value.ToString(), out i);
            return isSuccess;
        }
        public static bool IsNotEmpty(object value)
        {
            return value != null && value.ToString() != string.Empty;
        }
    }
}
