using System;
using System.Linq;

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
            return (T)source.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(T));
        }
    }
}