using System;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Routers;

namespace RoseByte.SharpArgs
{
    public class Cli
    {
        public static IRouter Router() => new Router();
        public static IRouter Router(IServiceCollection collection) => new Router(collection);
        public static IRouter Router(IServiceProvider provider) => new Router(provider);
        
        public static IAsyncRouter AsyncRouter() => new AsyncRouter();
        public static IAsyncRouter AsyncRouter(IServiceCollection collection) => new AsyncRouter(collection);
        public static IAsyncRouter AsyncRouter(IServiceProvider provider) => new AsyncRouter(provider);
    }
}