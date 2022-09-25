using System.Reflection;

namespace Reflectamundo.Asp
{
    public class HttpEndpoint
    {
        public string ControllerName { get; set; }

        public string Route { get; set; }


        public Assembly Assembly { get; set; }

        public ParameterInfo[] Parameters { get; set; }


        public MethodInfo MethodInfo { get; set; }


        public string ReturnType { get; set; }


        public Verbs Verb { get; set; }

        public enum Verbs { Get, Put, Post, Delete };
    }
}
