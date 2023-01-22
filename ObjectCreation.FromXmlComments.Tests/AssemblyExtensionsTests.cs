using BufTools.ObjectCreation.FromXmlComments.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflectamundo.Tests.Models;

namespace Reflectamundo.Tests
{
    [TestClass]
    public class AssemblyExtensionsTests
    {
        [TestMethod]
        public void GetTypes_WithInteracefaceType_GetsTypes()
        {
            var types = GetType().Assembly.GetTypes<ITestInterface>();

            Assert.IsNotNull(types);
            Assert.AreEqual(3 ,types.Count());
        }

        [TestMethod]
        public void GetTypes_WithAbstractType_GetsTypes()
        {
            var types = GetType().Assembly.GetTypes<AbstractBaseClass>();

            Assert.IsNotNull(types);
            Assert.AreEqual(2, types.Count());
        }

        [TestMethod]
        public void GetTypes_WithClassType_GetsTypes()
        {
            var types = GetType().Assembly.GetTypes<InterfaceClass>();

            Assert.IsNotNull(types);
            Assert.AreEqual(1, types.Count());
        }


        [TestMethod]
        public void GetClasses_WithInteracefaceType_GetsConcreteTypes()
        {
            var types = GetType().Assembly.GetClasses<ITestInterface>();

            Assert.IsNotNull(types);
            Assert.AreEqual(2, types.Count());
        }

        [TestMethod]
        public void GetClasses_WithAbstractType_GetsConcreteTypes()
        {
            var types = GetType().Assembly.GetClasses<AbstractBaseClass>();

            Assert.IsNotNull(types);
            Assert.AreEqual(1, types.Count());
        }


        [TestMethod]
        public void GetDirectoryPath_WithValidAssembly_GetsPath()
        {
            var path = GetType().Assembly.GetDirectoryPath();

            Assert.IsTrue(!string.IsNullOrEmpty(path));
        }
    }
}
