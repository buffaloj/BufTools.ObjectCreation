using BufTools.ObjectCreation.FromXmlComments;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreation.FromXmlComments.Tests.Attributes;
using ObjectCreation.FromXmlComments.Tests.BirthModels;
using System.Text.Json.Serialization;

namespace ObjectCreation.FromXmlComments.Tests
{
    [TestClass]
    public class ObjectMotherTests
    {
        private readonly ObjectMother _target;

        public ObjectMotherTests()
        {
            var sc = new ServiceCollection();
            sc.AddSingleton<DependencyInjectionModel>();
            sc.AddSingleton<DependencyModel>();
            var provider = sc.BuildServiceProvider();

            _target = new ObjectMother(provider);
            _target.IgnorePropertiesWithAttribute<JsonIgnoreAttribute>();
            _target.IgnorePropertiesWithAttribute<CustomIgnoreAttribute>();
        }

        [TestMethod]
        public void Birth_WithBasicPropertiesModel_CreatesObject()
        {
            var model = _target.Birth<BasicPropertiesModel>();

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.StringProperty);
            Assert.AreEqual(model.StringProperty, "A String!");
            Assert.AreEqual(model.FloatProperty, 6.6f);
            Assert.AreEqual(model.IntProperty, 8);
            Assert.AreEqual(model.BoolProperty, true);
        }

        [TestMethod]
        public void Birth_WithCollectionModel_CreatesObject()
        {
            var model = _target.Birth<CollectionModel>();

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Collection);
        }

        [TestMethod]
        public void Birth_WithDependencyInjectionModel_CreatesObject()
        {
            var model = _target.Birth<DependencyInjectionModel>();

            Assert.IsNotNull(model);
            Assert.AreEqual(model.ExampleFloat, 9.9f);
        }

        [TestMethod]
        public void Birth_WithIgnoredProperties_DoesNotSetValue()
        {
            var model = _target.Birth<IgnoreModel>();

            Assert.IsNotNull(model);
            Assert.IsNull(model.Ignore);
            Assert.IsNull(model.Ignore2);
            Assert.IsNull(model.Ignore3);
            Assert.IsNull(model.Ignore4);
            Assert.AreEqual(model.IntExample, 99);
        }
    }
}
