using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderExcercise.ViewModels;
using System.Runtime.CompilerServices;

namespace StockTraderExcercise.Helpers
{
    /// <summary>
    /// Experimenting..
    /// </summary>
    public static class DataAnnotationsValidator
    {
        public static bool TryValidateAll(object @object, out List<ValidationResult> results)
        {
            var context = new ValidationContext(@object, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                @object, context, results,
                validateAllProperties: true
            );
        }

        public static bool TryValidate(object @object, out List<ValidationResult> results, [CallerMemberName] string propertyName = null)
        {
            var context = new ValidationContext(@object, serviceProvider: null, items: null)
            {
                MemberName = propertyName
            };
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                @object, context, results
            );
        }
    }
}
