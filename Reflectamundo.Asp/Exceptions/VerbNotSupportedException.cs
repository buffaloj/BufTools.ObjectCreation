using System;
using System.Reflection;

namespace Reflectamundo.Asp.Exceptions
{
    public class VerbNotSupportedException : Exception
    {
        public VerbNotSupportedException(MethodInfo methodInfo)
            : base($"{methodInfo.DeclaringType.Name}.{methodInfo.Name} does not have an Http Verb Attribute(HttpGet, HttpPut, HttpPost, HttpDelete)")
        {
        }
    }
}
