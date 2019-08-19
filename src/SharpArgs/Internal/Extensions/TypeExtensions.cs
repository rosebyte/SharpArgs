using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using RoseByte.SharpArgs.Attributes;
using RoseByte.SharpArgs.Internal.Properties;

[assembly: InternalsVisibleTo("RoseByte.SharpArgs.Tests.dll")]
namespace RoseByte.SharpArgs.Internal.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool HasAttribute<T>(this Type source) where T : Attribute
        {
            return source.GetCustomAttributes(true).Any(x => x.GetType() == typeof(T));
        }
        
        internal static T GetAttribute<T>(this Type source) where T : Attribute
        {
            return (T)source.GetCustomAttribute(typeof(T));
        }

        internal static IEnumerable<Property> ExtractProperties(this object route)
        {
            return route.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(x => !x.HasAttribute<IgnoreAttribute>())
                .Select(x => new Property(route, x));
        }
    }
}