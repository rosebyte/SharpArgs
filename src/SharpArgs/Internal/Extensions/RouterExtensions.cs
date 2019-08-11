using System.Linq;
using System.Threading.Tasks;
using RoseByte.SharpArgs.Internal.Exceptions;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;

namespace RoseByte.SharpArgs.Internal.Extensions
{
    public static class RouterExtensions
    {
        public static void Run(this Router<IRoute> router, string[] args)
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
        
        public static async Task Run(this Router<IAsyncRoute> router, string[] args)
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