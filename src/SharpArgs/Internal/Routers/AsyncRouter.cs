using System;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

namespace RoseByte.SharpArgs.Internal.Routers
{
    public class AsyncRouter : Router<IAsyncRoute>, IAsyncRouter
    {
        internal AsyncRouter() { }
        internal AsyncRouter(IServiceCollection collection) : base(collection.UseAsyncRouter()) { }
        internal AsyncRouter(IServiceProvider provider) : base(provider) { }
    }
}