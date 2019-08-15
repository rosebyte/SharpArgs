using System.Linq;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Parser.Options;

// ReSharper disable once CheckNamespace
namespace RoseByte.SharpArgs
{
    public static class ResultExtensions
    {
        public static IResult<T> Bind<T>(this IResult<T> result, IParsingOptions options = null)
        {
            if (options == null)
            {
                options = new ParsingOptions();
            }
            
            if (result.Success)
            {
                var parser = (ICliParser<T>)result.Provider.GetService(typeof(ICliParser<T>));
                parser.Register(result);
                parser.Parse(result.CurrentArgs.ToList(), options);
            }

            return result;
        }
    }
}