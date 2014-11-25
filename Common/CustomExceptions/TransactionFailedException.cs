using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomExceptions
{
    public class TransactionFailedException : Exception
    {
        public TransactionFailedException()
            : base("The database transaction has failed. Please contact the administrator.") { }

        public TransactionFailedException(string message)
            : base(message) { }

        public TransactionFailedException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
