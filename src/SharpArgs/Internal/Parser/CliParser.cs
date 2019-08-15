using System;
using System.Collections.Generic;
using System.Linq;
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
                .Create(result.Route);
            _provider = result.Provider;
        }
        
        public void Parse(IReadOnlyList<string> args, IParsingOptions options)
        {
            var arguments = args
                .TakeWhile(arg => !arg.StartsWith(options.OptionPrefix) && !arg.StartsWith(options.FlagPrefix))
                .ToList();
            
            ((IOptionsParser)_provider.GetService(typeof(IOptionsParser)))
                .ParseParams(args.Skip(arguments.Count).ToList(), options, _helper);
            ((IArgumentsParser)_provider.GetService(typeof(IArgumentsParser)))
                .ParseArgs(arguments, _helper);
        }
    }
}