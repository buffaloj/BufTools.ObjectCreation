using System;
using System.Runtime.Serialization;

namespace Reflectamundo.Exceptions
{
    public class ValueNotFoundException : Exception
    {
        public ValueNotFoundException()
        {
        }

        public ValueNotFoundException(string message) : base(message)
        {
        }

        public ValueNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
