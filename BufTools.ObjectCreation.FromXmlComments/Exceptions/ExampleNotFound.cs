using System;
using System.Runtime.Serialization;

namespace BufTools.ObjectCreation.FromXmlComments.Exceptions
{
    public class ExampleNotFound : Exception
    {
        public ExampleNotFound()
        {
        }

        public ExampleNotFound(string message) : base(message)
        {
        }

        public ExampleNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExampleNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
