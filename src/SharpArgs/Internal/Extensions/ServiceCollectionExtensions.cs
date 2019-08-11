using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Attributes;
using RoseByte.SharpArgs.Internal.Exceptions;
using RoseByte.SharpArgs.Internal.Helpers;

namespace RoseByte.SharpArgs.Internal.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection Process(IServiceCollection collection, 
            Assembly[] assemblies, Type iface)
        {
            if (collection.Any(x => x.ServiceType == typeof(TypeHelper)))
            {
                throw new SharpArgsException(Constants.Exceptions.ServiceCollectionAlreadyUsed);
            }
            
            if (!assemblies.Any())
            {
                assemblies = new []{Assembly.GetCallingAssembly()};
            }
            
            var types = assemblies
                .Distinct()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.HasAttribute<IgnoreAttribute>())
                .Where(iface.IsAssignableFrom)
                .Where(x => x != iface)
                .ToList();

            collection.AddSingleton(new TypeHelper(types));
            
            if (types.All(x => !x.HasAttribute<DefaultAttribute>()))
            {
                collection.AddTransient<Help>();
            }

            foreach (var type in types)
            {
                collection.AddTransient(type);
            }
            
            return collection;
        }
        
        public static IServiceCollection UseRouter(this IServiceCollection collection, 
            params Assembly[] assemblies)
        {
            return Process(collection, assemblies, typeof(IRoute));
        }
        
        public static IServiceCollection UseAsyncRouter(this IServiceCollection collection, 
            params Assembly[] assemblies)
        {
            return Process(collection, assemblies, typeof(IAsyncRoute));
        }
    }
}