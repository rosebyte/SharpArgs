using System.Linq;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Parser;

// ReSharper disable once CheckNamespace
namespace RoseByte.SharpArgs
{
    public static class ResultExtensions
    {
        public static IResult<T> Bind<T>(this IResult<T> result, IReadOnlyParsingOptions options)
        {
            if (result.Success)
            {
                var parser = (ICliParser)result.Provider.GetService(typeof(ICliParser));
                var properties = TypeExtensions.ExtractProperties(result.Route);
                parser.Scan(properties);
                parser.Parse(result.CurrentArgs.ToList(), options);
            }

            return result;
        }
    }
}