using System;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

namespace RoseByte.SharpArgs.Internal.Routers
{
    internal class AsyncRouter : Router<IAsyncRoute>, IAsyncRouter
    {
        internal AsyncRouter(IServiceProvider provider) : base(provider) { }
    }
}