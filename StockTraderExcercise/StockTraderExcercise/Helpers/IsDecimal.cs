using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockTraderExcercise.Helpers
{
    /// <summary>
    /// Experimenting..
    /// </summary>
    class IsDecimal : ValidationAttribute 
    {
        string message;
        Regex regex = new Regex("[^0-9.-]+");
        public IsDecimal(string message = "is not a valid decimal")
        {
            this.message = message;
        }
        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
           
            if (value != null)
            {
                var isDecimal = !regex.IsMatch(value.ToString());
                if (!isDecimal)
                {
                    var errorMessage = FormatErrorMessage($@"{validationContext.DisplayName} : {this.message}");
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
