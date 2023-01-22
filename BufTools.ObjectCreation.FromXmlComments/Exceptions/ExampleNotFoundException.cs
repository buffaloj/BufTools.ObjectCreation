using System;
using System.Runtime.Serialization;

namespace Reflectamundo.Exceptions
{
    public class ExampleNotFoundException : Exception
    {
        public ExampleNotFoundException()
        {
        }

        public ExampleNotFoundException(string message) : base(message)
        {
        }

        public ExampleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExampleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
