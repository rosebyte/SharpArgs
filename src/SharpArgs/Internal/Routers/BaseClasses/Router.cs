using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Attributes;
using RoseByte.SharpArgs.Internal.Exceptions;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Helpers;
using RoseByte.SharpArgs.Internal.Parser;

namespace RoseByte.SharpArgs.Internal.Routers.BaseClasses
{
    public class Router<TRoute> : IRouter<TRoute>
    {
        private readonly ParsingOptions _options;
        public IReadOnlyParsingOptions Options => _options;
        private readonly Dictionary<string, Type> _routes;
        private Type _default;
        private readonly IServiceProvider _provider;

        protected Router() : this(new ServiceCollection()) { }
        protected Router(IServiceCollection collection) : this(collection.BuildServiceProvider()) { }
        protected Router(IServiceProvider provider)
        {
            _routes = new Dictionary<string, Type>();
            _provider = provider;
            _options = new ParsingOptions();
            RegisterRoutes(provider);
        }
        
        public bool TryGetRoute<T>(string name, out T route, out bool defaultUsed)
        {
            defaultUsed = false;
            route = default;
            
            if (_routes.TryGetValue(name, out var type))
            {
                route = (T)_provider.GetService(type);
                return true;
            }

            if (_default != null)
            {
                defaultUsed = true;
                route = (T) _provider.GetService(_default);
                return true;
            }
            
            return false;
        }

        private void RegisterRoutes(IServiceProvider provider)
        {
            foreach (var type in provider.GetService<TypeHelper>().Types)
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
        
        public IRouter<TRoute> WithOptionPrefix(string prefix)
        {
            _options.OptionPrefix = prefix.ToLower();
            return this;
        }

        public IRouter<TRoute> WithFlagPrefix(string prefix)
        {
            _options.FlagPrefix = prefix.ToLower();
            return this;
        }

        public IRouter<TRoute> WithCombinedFlagsAllowed()
        {
            _options.CanCombineFlags = true;
            return this;
        }

        public IRouter<TRoute> WithOptionsDelimitedBy(bool includeSpace, params char[] delimiters)
        {
            if (includeSpace && !delimiters.Contains(' '))
            {
                delimiters.Append(' ');
            }
            
            _options.Delimiters = delimiters;
            return this;
        }
        
        public IRouter<TRoute> WithDefault<T>() where T: TRoute
        {
            _default = typeof(T);
            return this;
        }
        
        public IRouter<TRoute> WithRoute<T>(string name = null) where  T: TRoute
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = typeof(T).Name;
            }
            
            _routes.Add(name.ToLower(), typeof(T));
            return this;
        }
    }
}