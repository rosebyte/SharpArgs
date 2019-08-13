using System;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Routers;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

namespace RoseByte.SharpArgs
{
    public static class Cli
    {
        public static IRouter<T> Router<T>()
        {
            var provider = new ServiceCollection().UseSharpArgs<T>().BuildServiceProvider();
            return provider.GetService<Router<T>>();
        }

        public static IRouter<T> Router<T>(IServiceProvider provider) => provider.GetService<IRouter<T>>();
    }
}