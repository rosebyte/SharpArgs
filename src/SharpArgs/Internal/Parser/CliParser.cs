using System;
using System.Collections.Generic;
using System.Linq;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Parser.Options;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public class CliParser<T> : ICliParser<T>
    {
        private IParsingHelper _helper;
        private IServiceProvider _provider;

        public void Register(IResult<T> result)
        {
            _helper = ((IParsingHelperFactory)result.Provider.GetService(typeof(IParsingHelperFactory)))
                .Create(result.Route.ExtractProperties());
            _provider = result.Provider;
        }
        
        public void Parse(IReadOnlyList<string> args, IReadOnlyParsingOptions options)
        {
            var arguments = args
                .TakeWhile(arg => !arg.StartsWith(options.OptionPrefix) && !arg.StartsWith(options.FlagPrefix))
                .ToList();
            
            ((OptionsParser)_provider.GetService(typeof(OptionsParser)))
                .ParseParams(args.Skip(arguments.Count).ToList(), options, _helper);
            ((ArgumentsParser)_provider.GetService(typeof(ArgumentsParser)))
                .ParseArgs(arguments, _helper);
        }
    }
}