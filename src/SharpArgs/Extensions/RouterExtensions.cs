using System.Linq;
using System.Threading.Tasks;
using RoseByte.SharpArgs.Exceptions;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

// ReSharper disable once CheckNamespace
namespace RoseByte.SharpArgs
{
    public static class RouterExtensions
    {
        public static void Run(this IRouter<IRoute> router, string[] args)
        {
            var value = args.FirstOrDefault()?.ToLower();
            
            if (!router.TryGetRoute<IRoute>(value, out var route, out var defaultUsed))
            {
                throw new SharpArgsException($"No suitable route found for: {value ?? "(null)"}");
            }
            
            new CliParser(route.Properties(), router.Options)
                .Parse(args.Skip(defaultUsed ? 0 : 1).ToList());
            
            route.Execute();
        }
        
        public static async Task Run(this IRouter<IAsyncRoute> router, string[] args)
        {
            var value = args.FirstOrDefault()?.ToLower();

            if (!router.TryGetRoute<IAsyncRoute>(value, out var route, out var defaultUsed))
            {
                throw new SharpArgsException($"No suitable route found for: {value ?? "(null)"}");
            }
            
            new CliParser(route.Properties(), router.Options)
                .Parse(args.Skip(defaultUsed ? 0 : 1).ToList());
            await route.Execute();
        }
    }
}