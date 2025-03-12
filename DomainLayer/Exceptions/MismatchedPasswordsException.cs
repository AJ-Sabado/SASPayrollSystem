using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class MismatchedPasswordsException : Exception
    {
        public MismatchedPasswordsException()
        {
        }

        public MismatchedPasswordsException(string? message) : base(message)
        {
        }

        public MismatchedPasswordsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MismatchedPasswordsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
