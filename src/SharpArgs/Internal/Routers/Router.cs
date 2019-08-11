using System;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

namespace RoseByte.SharpArgs.Internal.Routers
{
    internal class Router : Router<IRoute>, IRouter
    {
        internal Router(IServiceProvider provider) : base(provider) { }
    }
}