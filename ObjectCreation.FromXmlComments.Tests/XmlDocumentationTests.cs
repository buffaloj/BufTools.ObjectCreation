using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using ObjectCreation.FromXmlComments.Tests.ReflectionModels;
using BufTools.Extensions.XmlComments;

namespace ObjectCreation.FromXmlComments.Tests
{
    [TestClass]
    public class XmlDocumentationTests
    {
        private readonly Assembly _assembly;

        public XmlDocumentationTests()
        {
            _assembly = typeof(XmlCommentModel).Assembly;
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
            var objectComments = docs.GetDocumentation(typeof(XmlCommentModel));

            Assert.IsNotNull(objectComments);
        }

        [TestMethod]
        public void GetDocumentation_WithValidMethod_GetsDocumentation()
        {
            var docs = _assembly.LoadXmlDocumentation();
            var objectComments = docs.GetDocumentation(typeof(XmlCommentModel));

            Assert.IsNotNull(objectComments);
        }
    }
}
