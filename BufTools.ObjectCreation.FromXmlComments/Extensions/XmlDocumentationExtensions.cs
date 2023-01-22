﻿using BufTools.ObjectMother.FromXmlComments.Resources;
using Reflectamundo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace BufTools.ObjectCreation.FromXmlComments.Extensions
{
    public static class XmlDocumentationExtensions
    {
        public static IDictionary<string, MemberDoc> LoadXmlDocumentation(this Assembly assembly)
        {
            var xmlDirectoryPath = assembly.GetDirectoryPath();
            string xmlFilePath = Path.Combine(xmlDirectoryPath, assembly.GetName().Name + ".xml");

            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException(string.Format(ReflectamundoResources.XmlFileNotFound, xmlFilePath));

            return LoadXmlDocumentation(File.ReadAllText(xmlFilePath));
        }

        public static IDictionary<string, MemberDoc> LoadXmlDocumentation(string xmlDocumentation)
        {
            var loadedXmlDocumentation = new Dictionary<string, MemberDoc>();
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xmlDocumentation)))
            {
                MemberDoc currentDoc = null;
                while (xmlReader.Read())
                {
                    if (xmlReader.Name == "member")
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            currentDoc = new MemberDoc();
                            string raw_name = xmlReader["name"];
                            loadedXmlDocumentation[raw_name] = currentDoc;
                        }
                        else if (xmlReader.NodeType == XmlNodeType.Element)
                            currentDoc = null;
                    }

                    if (currentDoc != null)
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "summary")
                            currentDoc.Summary = xmlReader.ReadInnerXml();

                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "example")
                            currentDoc.Example = xmlReader.ReadInnerXml();

                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "param")
                        {
                            var parameter = new ParamDoc();
                            currentDoc.Params.Add(parameter);
                            parameter.Name = xmlReader["name"];
                            parameter.Example = xmlReader["example"];
                        }
                    }
                }
            }
            return loadedXmlDocumentation;
        }

        public static MemberDoc GetDocumentation(this IDictionary<string, MemberDoc> xmlDocumentation, Type type)
        {
            var key = "T:" + XmlDocumentationKeyHelper(type.FullName, null);
            xmlDocumentation.TryGetValue(key, out MemberDoc documentation);
            return documentation;
        }

        public static MemberDoc GetDocumentation(this IDictionary<string, MemberDoc> xmlDocumentation, PropertyInfo propertyInfo)
        {
            var key = "P:" + XmlDocumentationKeyHelper(propertyInfo.DeclaringType.FullName, propertyInfo.Name);
            xmlDocumentation.TryGetValue(key, out MemberDoc documentation);
            return documentation;
        }

        public static MemberDoc GetDocumentation(this Dictionary<string, MemberDoc> xmlDocumentation, MethodInfo methodInfo)
        {
            string key;
            if (methodInfo.IsConstructor)
                key = "M:" + XmlDocumentationKeyHelper(methodInfo.DeclaringType.FullName, null);
            else
            {
                key = "M:" + XmlDocumentationKeyHelper(methodInfo.DeclaringType.FullName, methodInfo.Name);
                var parameters = methodInfo.GetParameters();
                if (parameters.Any())
                    key += "(" + string.Join(",", parameters.Select(p => p.ParameterType.FullName)) + ")";
            }

            xmlDocumentation.TryGetValue(key, out MemberDoc documentation);
            return documentation;
        }

        private static string XmlDocumentationKeyHelper(string typeFullNameString, string memberNameString)
        {
            string key = Regex.Replace(typeFullNameString, @"\[.*\]", string.Empty)
                              .Replace('+', '.');
            if (memberNameString != null)
                key += "." + memberNameString;

            return key;
        }
    }
}
