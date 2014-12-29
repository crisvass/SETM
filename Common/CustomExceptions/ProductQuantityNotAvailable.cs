using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomExceptions
{
    public class ProductQuantityNotAvailable : Exception
    {
        public ProductQuantityNotAvailable()
            : base("The product quantity requested is not available.") { }

        public ProductQuantityNotAvailable(string message)
            : base(message) { }

        public ProductQuantityNotAvailable(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
