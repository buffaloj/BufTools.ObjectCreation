using System;
using System.Runtime.Serialization;

namespace BufTools.ObjectCreation.FromXmlComments.Exceptions
{
    public class CannotInstantiateType : Exception
    {
        public CannotInstantiateType()
        {
        }

        public CannotInstantiateType(string message) : base(message)
        {
        }

        public CannotInstantiateType(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotInstantiateType(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
