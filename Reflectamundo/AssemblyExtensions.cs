using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Reflectamundo
{
    /// <summary>
    /// A set of convenience methods for performing common reflection operations
    /// </summary>
    public static class AssemblyExtensions
    {
        public static Type[] GetTypes<T>(this Assembly assembly)
            where T : class
        {
            return assembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t)).ToArray();
        }

        public static Type[] GetClasses<T>(this Assembly assembly)
            where T : class
        {
            return assembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && 
                                                  !t.IsInterface && 
                                                  !t.IsAbstract)
                           .ToArray();
        }

        public static string GetDirectoryPath(this Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
