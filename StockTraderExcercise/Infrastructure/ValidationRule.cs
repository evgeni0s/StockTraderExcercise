using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ValidationRule 
    {
        public string Message { get; set; }
        public Func<object, bool> Validation { get; set; }

        public bool IsDirty { get; set; }
    }
}
