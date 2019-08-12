using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Attributes;
using RoseByte.SharpArgs.Exceptions;
using RoseByte.SharpArgs.Internal;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Helpers;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

// ReSharper disable once CheckNamespace
namespace RoseByte.SharpArgs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseSharpArgs<T>(this IServiceCollection collection, 
            params Assembly[] assemblies)
        {
            if (collection.Any(x => x.ServiceType == typeof(ITypeHelper<T>)))
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
                .Where(typeof(T).IsAssignableFrom)
                .Where(x => !x.HasAttribute<IgnoreAttribute>())
                .Where(x => x != typeof(T))
                .ToList();
            
            foreach (var type in types)
            {
                collection.AddTransient(type);
            }
            
            collection.AddSingleton<ITypeHelper<T>>(new TypeHelper<T>(types));
            collection.AddTransient<ICliParser, CliParser>();
            collection.AddTransient<IRouter<T>, Router<T>>();
            
            return collection;
        }
    }
}