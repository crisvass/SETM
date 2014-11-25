using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomExceptions
{
    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException()
            : base("Username does not exist.") { }

        public InvalidUsernameException(string message)
            : base(message) { }

        public InvalidUsernameException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
