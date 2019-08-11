using System;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Routers;

namespace RoseByte.SharpArgs
{
    public static class Cli
    {
        public static IRouter Router()
        {
            var provider = new ServiceCollection().UseRouter().BuildServiceProvider();
            return provider.GetService<Router>();
        }

        public static IRouter Router(IServiceProvider provider) => provider.GetService<IRouter>();
        
        public static IAsyncRouter AsyncRouter()
        { 
            var provider = new ServiceCollection().UseRouter().BuildServiceProvider();
            return provider.GetService<AsyncRouter>();
        }

        public static IAsyncRouter AsyncRouter(IServiceProvider provider) => provider.GetService<IAsyncRouter>();
    }
}