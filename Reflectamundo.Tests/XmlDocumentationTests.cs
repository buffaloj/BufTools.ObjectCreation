using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Reflectamundo.TestWebApi.Controllers;
using Reflectamundo.TestWebApi.Requests;

namespace Reflectamundo.Tests
{
    [TestClass]
    public class XmlDocumentationTests
    {
        private readonly Assembly _assembly;

        public XmlDocumentationTests()
        {
            _assembly = typeof(ExampleController).Assembly;
        }

        [TestMethod]
        public void LoadXmlDocumentation_WithValidAssembly_LoadsDocumentation()
        {
            var docs = _assembly.LoadXmlDocumentation();

            Assert.IsNotNull(docs);
        }

        [TestMethod]
        public void GetDocumentation_WithValidType_GetsDocumentation()
        {
            var docs = _assembly.LoadXmlDocumentation();
            var objectComments = docs.GetDocumentation(typeof(ExampleRequest));

            Assert.IsNotNull(objectComments);
        }

        [TestMethod]
        public void GetDocumentation_WithValidMethod_GetsDocumentation()
        {
            var docs = _assembly.LoadXmlDocumentation();
            var objectComments = docs.GetDocumentation(typeof(ExampleController));

            Assert.IsNotNull(objectComments);
        }
    }
}
