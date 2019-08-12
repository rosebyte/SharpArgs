using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RoseByte.SharpArgs.Tests")]
namespace RoseByte.SharpArgs.Internal.Extensions
{
    internal static class PropertyInfoExtensions
    {
        public static T GetAttribute<T>(this PropertyInfo info) where T : Attribute
        {
            return (T)info.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(T));
        }
        
        public static bool HasAttribute<T>(this PropertyInfo info) where T : Attribute
        {
            return info.GetCustomAttributes(true).Any(x => x.GetType() == typeof(T));
        }
    }
}