namespace ObjectCreation.FromXmlComments.Tests.BirthModels
{
    public class DependencyInjectionModel
    {
        private readonly DependencyModel _model;

        public DependencyInjectionModel(DependencyModel model) => _model = model;

        /// <summary>
        /// An example of a floating point property
        /// </summary>
        /// <example>9.9</example>
        public float ExampleFloat { get; set; }
    }
}
