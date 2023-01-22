namespace ObjectCreation.FromXmlComments.Tests.ReflectionModels
{
    /// <summary>
    /// A class that has a rangle examples of properties and methods with XML documentation
    /// </summary>
    public class XmlCommentModel
    {
        /// <summary>
        /// A example of a string
        /// </summary>
        public string ExampleString { get; set; }

        /// <summary>
        /// An example of a method that returns nothing and doesn't take params
        /// </summary>
        public void ExampleVoidMethod()
        {
        }

        /// <summary>
        /// An example of a method that returns nothing and takes a param
        /// </summary>
        /// <param name="param">An example param</param>
        public void ExampleVoidMethodWithParam(int param)
        {

        }

        /// <summary>
        /// An example of a method that returns an int
        /// </summary>
        /// <returns>An integer</returns>
        public int ExampleIntMethod()
        {
            return 5;
        }

        /// <summary>
        ///  An example of a method that returns an int and accepts a param
        /// </summary>
        /// <param name="param">An example param</param>
        /// <returns>An integer</returns>
        public int ExampleIntMethod(int param)
        {
            return param;
        }
    }
}
