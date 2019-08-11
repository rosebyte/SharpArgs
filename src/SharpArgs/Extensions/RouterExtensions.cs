using System.Linq;
using System.Threading.Tasks;
using RoseByte.SharpArgs.Exceptions;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;
using RoseByte.SharpArgs.Internal.Routes.BaseClasses;

// ReSharper disable once CheckNamespace
namespace RoseByte.SharpArgs
{
    public static class RouterExtensions
    {
        public static void Run(this IRouter<IRoute> router, string[] args)
        {
            GetRoute(router, args).Execute();
        }
        
        public static async Task Run(this IRouter<IAsyncRoute> router, string[] args)
        {
            await GetRoute(router, args).Execute();
        }

        private static T GetRoute<T>(IRouter<T> router, string[] args) where T : IRouteBase
        {
            var value = args.FirstOrDefault()?.ToLower();
            
            if (!router.TryGetRoute<T>(value, out var route, out var defaultUsed))
            {
                throw new SharpArgsException($"No suitable route found for: {value ?? "(null)"}");
            }
            
            var parser = router.GetParser();
            var properties = route.Properties();
            parser.Scan(properties);
            parser.Parse(args.Skip(defaultUsed ? 0 : 1).ToList(), router.Options);

            return route;
        }
    }
}