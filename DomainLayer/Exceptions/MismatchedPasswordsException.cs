using System.Runtime.Serialization;

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
