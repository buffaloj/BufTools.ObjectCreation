namespace ObjectCreation.FromXmlComments.Tests.BirthModels
{
    /// <summary>
    /// A model that another class is dependent on thru it's constructor
    /// </summary>
    public class DependencyModel
    {
        /// <summary>
        /// An example int id
        /// </summary>
        /// <example>18</example>
        public int Id { get; set; }
    }
}
