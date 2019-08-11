using System;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

namespace RoseByte.SharpArgs.Internal.Routers
{
    public class Router : Router<IRoute>, IRouter
    {
        internal Router() { }
        internal Router(IServiceCollection collection) : base(collection.UseRouter()) { }
        internal Router(IServiceProvider provider) : base(provider) { }
    }
}