using ObjectCreation.FromXmlComments.Tests.Attributes;
using System.Text.Json.Serialization;

namespace ObjectCreation.FromXmlComments.Tests.BirthModels
{
    /// <summary>
    /// A test model that includes JsonIgnoreAttribute
    /// </summary>
    public class IgnoreModel
    {
        [JsonIgnore]
        public string Ignore { get; set; }

        [CustomIgnore]
        public string Ignore2 { get; set; }

        /// <summary>
        /// XML comment with an example
        /// </summary>
        /// <example>Ignore3!</example>
        [JsonIgnore]
        public string Ignore3 { get; set; }

        /// <summary>
        /// XML comment with an example
        /// </summary>
        /// <example>Ignore4!</example>
        [CustomIgnore]
        public string Ignore4 { get; set; }

        /// <summary>
        /// An example int
        /// </summary>
        /// <example>99</example>
        public int IntExample { get; set; }
    }
}
