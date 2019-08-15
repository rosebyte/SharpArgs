using System;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Routers;

namespace RoseByte.SharpArgs
{
    public static class Cli
    {
        public static IRouter<T> Router<T>()
        {
            var provider = new ServiceCollection().UseSharpArgs<T>().BuildServiceProvider();
            return new Router<T>(provider);
        }

        public static IRouter<T> Router<T>(IServiceProvider provider) => new Router<T>(provider);
    }
}