using System.Collections.Generic;
using System.Linq;
using RoseByte.SharpArgs.Attributes;
using RoseByte.SharpArgs.Internal.Properties;
using RoseByte.SharpArgs.Internal.Routes.BaseClasses;

namespace RoseByte.SharpArgs.Internal.Extensions
{
    internal static class RouteBaseExtensions
    {
        internal static IEnumerable<Property> Properties(this IRouteBase route)
        {
            return route.GetType().GetProperties()
                .Where(x => !x.HasAttribute<IgnoreAttribute>())
                .Select(x => new Property(route, x));
        }
    }
}