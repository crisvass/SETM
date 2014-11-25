using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomExceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
            : base("Password does not match the specified username.") { }

        public InvalidPasswordException(string message)
            : base(message) { }

        public InvalidPasswordException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
