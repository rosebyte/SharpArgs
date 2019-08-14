using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Helpers;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

namespace RoseByte.SharpArgs.Internal.Routers
{
    public class Router<TRoute> : IRouter<TRoute>
    {
        private readonly IReadOnlyDictionary<string, Type> _routes;
        private readonly IServiceProvider _provider;

        internal Router(IServiceProvider provider)
        {
            _provider = provider;
            _routes = provider.GetService<ITypeHelper<TRoute>>().Types;
        }

        public IResult<TRoute> Resolve(string[] args)
        {
            var name = args.FirstOrDefault()?.ToLower() ?? string.Empty;
            
            var result = new Result<TRoute>
            {
                OriginalArgs = args,
                CurrentArgs = args,
                Provider = _provider
            };
            
            if (_routes.TryGetValue(name, out var type))
            {
                result.Route = (TRoute) _provider.GetService(type);
                result.CurrentArgs = args.Skip(1).ToArray();
                result.Success = true;
            }

            return result;
        }
    }
}