using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflectamundo.Tests.BirthModels;

namespace Reflectamundo.Tests
{
    [TestClass]
    public class ObjectMotherTests
    {
        private readonly ObjectMother _target;

        public ObjectMotherTests()
        {
            var sc = new ServiceCollection();
            sc.AddSingleton<BasicPropertiesModel>();
            sc.AddSingleton<CollectionModel>();
            var provider = sc.BuildServiceProvider();

            _target = new ObjectMother(provider);
        }

        [TestMethod]
        public void Birth_WithBasicPropertiesModel_CreatesObject()
        {
            var model = _target.Birth(typeof(BasicPropertiesModel));

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Birth_WithCollectionModel_CreatesObject()
        {
            var model = _target.Birth(typeof(CollectionModel));

            Assert.IsNotNull(model);
        }
    }
}
