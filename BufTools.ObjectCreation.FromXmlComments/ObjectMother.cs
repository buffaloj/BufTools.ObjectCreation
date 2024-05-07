using BufTools.ObjectCreation.FromXmlComments.Exceptions;
using BufTools.Extensions.XmlComments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BufTools.ObjectCreation.FromXmlComments
{
    /// <summary>
    /// A class that instantiates an object and uses the objects XML comment example values to initializes it
    /// </summary>
    public class ObjectMother
    {
        private readonly IServiceProvider _provider;
        private readonly IList<Type> _ignoreAttributes = new List<Type>();

        /// <summary>
        /// Initializes an object instance with an optional service provider
        /// </summary>
        /// <param name="provider">A service provider to create an object.</param>
        /// <remarks>If the provider fails to create an object, then Activator will be used instead.</remarks>
        /// <remarks>Using Activator means the object being constructed must have an public constructor that takes no arguments.</remarks>
        public ObjectMother(IServiceProvider provider = null)
        {
            _provider = provider;
        }

        /// <summary>
        /// Allows your to specify any Attributes where the property should be ignored. For ex, JsonIgnoreAttribute
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns>An <see cref="ObjectMother"/> instance for chaining</returns>
        public ObjectMother IgnorePropertiesWithAttribute<TAttribute>() where TAttribute : Attribute
        {
            _ignoreAttributes.Add(typeof(TAttribute));
            return this;
        }

        /// <summary>
        /// Instantiates an object of type T and initializes the properties to the example value found in the XML comments
        /// </summary>
        /// <typeparam name="T">The type of object to create</typeparam>
        /// <returns>An instance of the object initialized with example values</returns>
        public T Birth<T>()
        {
            return (T)Birth(typeof(T));
        }

        /// <summary>
        /// Instantiates an object and initializes the properties to the example value found in the XML comments
        /// </summary>
        /// <param name="type">The type of object to create</param>
        /// <param name="reportError">Delegate usd to record errors</param>
        /// <returns>An instance of the object initialized with example values</returns>
        /// <exception cref="CannotInstantiateType">Thrown when the object cannot be instantiated</exception>
        /// <exception cref="ExampleNotFound">When an XML example value was not provided on a property</exception>
        /// <exception cref="ValueNotFound">Thrown when a value is not found on in an example</exception>
        /// <exception cref="XmlDocumentationFileNotFound">Thrown when the XML documentation file on disk was not found/does not exist.</exception>
        public object Birth(Type type, Action<Type, string> reportError = null)
        {
            var instance = _provider?.GetService(type);
            if (instance == null)
                instance = Activator.CreateInstance(type);

            if (instance == null)
            {
                reportError?.Invoke(type, $"Could not create an instance of {type}");
                return null;
            }

            var xmlDocs = type.Assembly.LoadXmlDocumentation();
            if (xmlDocs == null)
            {
                reportError?.Invoke(type, $"Could not load {type.Assembly.GetName().Name}.xml");
                return null;
            }

            foreach (var property in type.GetProperties())
            {
                if (_ignoreAttributes.Intersect(property.GetCustomAttributes().Select(a => a.GetType())).Any())
                    continue;

                // TODO: this came up due to DateTimeOffset - there is no XML comments for the DLL it lives in
                var doc = xmlDocs.GetDocumentation(property);
                if (doc == null)
                {
                    reportError?.Invoke(type, $"No XML documentation was found for {type.Name}.{property.Name}");
                    continue;
                }

                // TODO: this came up due to NAV properties - skip if it's a virtual object?
                if (doc.Example == null && !(property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    reportError?.Invoke(type, $"An example value is needed for property {type.Name}.{property.Name} (even if it's empty)");
                    continue;
                }

                var val = GetValue(doc.Example, property.PropertyType);
                property.SetValue(instance, val);
            }
            return instance;
        }

        private object GetValue(string value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                type = type.GetGenericArguments()[0];

                var innerObject = GetValue(value, type);// Birth(type);

                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(type);
                var list = Activator.CreateInstance(constructedListType) as IList;
                list?.Add(innerObject);
                return list;
            }

            if (type == typeof(string))
                return value;

            if (type == typeof(int?))
                return string.IsNullOrEmpty(value) ? default(int?) : int.Parse(value);
            if (type == typeof(int))
                return int.Parse(value);

            if (type == typeof(float?))
                return string.IsNullOrEmpty(value) ? default(float?) : float.Parse(value);
            if (type == typeof(float))
                return float.Parse(value);

            if (type == typeof(decimal?))
                return string.IsNullOrEmpty(value) ? default(decimal?) : decimal.Parse(value);
            if (type == typeof(decimal))
                return decimal.Parse(value);

            if (type == typeof(short?))
                return string.IsNullOrEmpty(value) ? default(short?) : short.Parse(value);
            if (type == typeof(short))
                return short.Parse(value);

            if (type == typeof(long?))
                return string.IsNullOrEmpty(value) ? default(long?) : long.Parse(value);
            if (type == typeof(long))
                return long.Parse(value);

            if (type == typeof(DateTime?))
                return string.IsNullOrEmpty(value) ? default(DateTime?) : DateTime.Parse(value);
            if (type == typeof(DateTime))
                return string.IsNullOrEmpty(value) ? DateTime.MinValue : DateTime.Parse(value);

            if (type == typeof(bool?))
                return string.IsNullOrEmpty(value) ? default(long?) : long.Parse(value);
            if (type == typeof(bool))
                return bool.Parse(value);

            if (type.IsEnum)
                return Convert.ChangeType(value, Enum.GetUnderlyingType(type));

            return Birth(type);
        }
    }
}
