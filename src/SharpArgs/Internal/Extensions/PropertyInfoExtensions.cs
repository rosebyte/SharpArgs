using System;
using System.Linq;
using System.Reflection;

namespace RoseByte.SharpArgs.Internal.Extensions
{
    public static class PropertyInfoExtensions
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