using System;
using System.Runtime.Serialization;

namespace BufTools.ObjectCreation.FromXmlComments.Exceptions
{
    public class ValueNotFound : Exception
    {
        public ValueNotFound()
        {
        }

        public ValueNotFound(string message) : base(message)
        {
        }

        public ValueNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
