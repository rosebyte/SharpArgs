using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Attributes;
using RoseByte.SharpArgs.Exceptions;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Helpers;

namespace RoseByte.SharpArgs.Internal.Routers.BaseClasses
{
    public class Router<TRoute> : IRouter<TRoute>
    {
        private readonly Dictionary<string, Type> _routes;
        private Type _default;
        private readonly IServiceProvider _provider;

        protected Router(IServiceProvider provider)
        {
            _routes = new Dictionary<string, Type>();
            _provider = provider;
            RegisterRoutes(provider);
        }

        private void RegisterRoutes(IServiceProvider provider)
        {
            foreach (var type in provider.GetService<ITypeHelper<TRoute>>().Types)
            {
                var route = type.GetAttribute<RouteAttribute>();
                if (route != null)
                {
                    foreach (var label in route.Labels)
                    {
                        _routes.Add(label.ToLower(), type);
                    }
                    
                    if (route.Default)
                    {
                        if (_default != null)
                        {
                            throw new SharpArgsException("There can be only one default route.");
                        }
                        
                        _default = type;
                    }
                }
                else
                {
                    _routes.Add(type.Name.ToLower(), type);
                }
            }
        }

        public IResult<TRoute> Resolve(string[] args)
        {
            var name = args.FirstOrDefault()?.ToLower();

            if (name != null && _routes.TryGetValue(name, out var type))
            {
                return new Result<TRoute>
                {
                    Route = (TRoute)_provider.GetService(type),
                    OriginalArgs = args,
                    CurrentArgs = args.Skip(1).ToArray(),
                    Success = true,
                    Provider = _provider
                };
            }

            if (_default != null)
            {
                return new Result<TRoute>
                {
                    Route = (TRoute)_provider.GetService(_default),
                    OriginalArgs = args,
                    CurrentArgs = args,
                    DefaultRouteUsed = true,
                    Success = true,
                    Provider = _provider
                };
            }
            
            return new Result<TRoute>
            {
                OriginalArgs = args,
                CurrentArgs = args,
                Provider = _provider
            };
        }
    }
}