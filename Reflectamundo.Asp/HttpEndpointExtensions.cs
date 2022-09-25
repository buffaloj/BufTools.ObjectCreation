using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Reflectamundo.Asp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflectamundo.Asp
{
    public static class HttpEndpointExtensions
    {
        public static IEnumerable<HttpEndpoint> GetEndpoints(this Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new HttpEndpoint
                {
                    ControllerName = x.DeclaringType.Name,
                    Route = MakeRoute(x.DeclaringType.GetCustomAttribute<RouteAttribute>(),
                                      x.GetCustomAttribute<RouteAttribute>(),
                                      x.GetCustomAttribute<HttpGetAttribute>(),
                                      x.GetCustomAttribute<HttpPutAttribute>(),
                                      x.GetCustomAttribute<HttpPostAttribute>(),
                                      x.GetCustomAttribute<HttpDeleteAttribute>(),
                                      x.GetCustomAttribute<HttpPatchAttribute>()),
                    Parameters = x.GetParameters(),
                    MethodInfo = x,
                    ReturnType = x.ReturnType.Name,
                    Assembly = x.DeclaringType.Assembly,
                    Verb = GetVerb(x)
                })
                .OrderBy(x => x.ControllerName).ThenBy(x => x.MethodInfo.Name).ToList();
        }

        private static string MakeRoute(params Attribute[] attributes)
        {
            var route = "";
            foreach (var attribute in attributes)
                route += attribute.Route();
            return route;
        }

        private static HttpEndpoint.Verbs GetVerb(MethodInfo info)
        {
            if (info.GetCustomAttribute<HttpGetAttribute>() != null)
                return HttpEndpoint.Verbs.Get;

            if (info.GetCustomAttribute<HttpPutAttribute>() != null)
                return HttpEndpoint.Verbs.Put;

            if (info.GetCustomAttribute<HttpPostAttribute>() != null)
                return HttpEndpoint.Verbs.Post;

            if (info.GetCustomAttribute<HttpDeleteAttribute>() != null)
                return HttpEndpoint.Verbs.Delete;

            throw new VerbNotSupportedException(info);
        }

        private static string Route(this Attribute attribute)
        {
            if (attribute == null)
                return "";

            if (attribute is RouteAttribute)
                return (attribute as RouteAttribute)?.Template.ToRouteSegment() ?? "";

            if (attribute is HttpMethodAttribute)
                return (attribute as HttpMethodAttribute)?.Template.ToRouteSegment() ?? "";

            return "";
        }

        private static string ToRouteSegment(this string route)
        {
            if (string.IsNullOrWhiteSpace(route))
                return "";

            return "/" + route.Trim(new char[] { '/', ' ', '\\' });
        }
    }
}
