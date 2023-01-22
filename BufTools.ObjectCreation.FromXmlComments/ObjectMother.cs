using BufTools.ObjectCreation.FromXmlComments.Extensions;
using Reflectamundo.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Reflectamundo
{
    public class ObjectMother
    {
        private readonly IServiceProvider _provider;

        public ObjectMother(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public T Birth<T>()
        {
            return (T)Birth(typeof(T));
        }

        public object Birth(Type type)
        {
            //if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            //{
            //    return GetValue("", type) ?? throw new ValueNotFoundException();
            //}

            var instance = _provider.GetService(type);
            if (instance == null)
                throw new TypeNotRegisteredException($"Could not create an instance of {type}");

            var xmlDocs = type.Assembly.LoadXmlDocumentation();
            if (xmlDocs == null)
                throw new Exception($"Could not load {type.Assembly.GetName().Name}.xml");

            foreach (var property in type.GetProperties())
            {
                //if (property.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any())
                //    continue;

                var doc = xmlDocs.GetDocumentation(property);
                if (doc == null)
                    throw new XmlDocumentationFileNotFoundException($"No XML documentation was found for {type.Name}.{property.Name}");

                if (doc.Example == null && !(property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                    throw new ExampleNotFoundException($"An example value is needed for property {type.Name}.{property.Name} (even if it's empty)");

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
                return DateTime.Parse(value);

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
