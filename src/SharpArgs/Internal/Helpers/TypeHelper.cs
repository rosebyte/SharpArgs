using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoseByte.SharpArgs.Attributes;
using RoseByte.SharpArgs.Internal.Extensions;

namespace RoseByte.SharpArgs.Internal.Helpers
{
    internal class TypeHelper<T> : ITypeHelper<T>
    {
        public IReadOnlyDictionary<string, Type> Types { get; }

        public TypeHelper(IReadOnlyList<Type> types)
        {
            Types = new ReadOnlyDictionary<string, Type>(GetDictionary(types));
        }

        private Dictionary<string, Type> GetDictionary(IEnumerable<Type> types)
        {
            var result = new Dictionary<string, Type>();
            
            foreach (var type in types)
            {
                var routeAttribute = type.GetAttribute<RouteAttribute>();
                if (routeAttribute != null)
                {
                    foreach (var label in routeAttribute.Labels)
                    {
                        result.Add(label.ToLower(), type);
                    }
                }
                else
                {
                    result.Add(type.Name.ToLower(), type);
                }
            }

            return result;
        }
    }
}