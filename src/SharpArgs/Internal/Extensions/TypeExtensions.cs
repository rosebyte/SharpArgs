using System;
using System.Linq;

namespace RoseByte.SharpArgs.Internal.Extensions
{
    public static class TypeExtensions
    {
        
        
        public static bool HasAttribute<T>(this Type source) where T : Attribute
        {
            return source.GetCustomAttributes(true).Any(x => x.GetType() == typeof(T));
        }
        
        public static T GetAttribute<T>(this Type source) where T : Attribute
        {
            return (T)source.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(T));
        }
    }
}