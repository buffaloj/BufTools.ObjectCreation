using System;
using System.Runtime.Serialization;

namespace Reflectamundo.Exceptions
{
    public class XmlDocumentationFileNotFoundException : Exception
    {
        public XmlDocumentationFileNotFoundException()
        {
        }

        public XmlDocumentationFileNotFoundException(string message) : base(message)
        {
        }

        public XmlDocumentationFileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XmlDocumentationFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
