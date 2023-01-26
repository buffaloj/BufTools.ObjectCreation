using System.Text.Json.Serialization;

namespace ObjectCreation.FromXmlComments.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CustomIgnoreAttribute : Attribute
    {
    }
}
