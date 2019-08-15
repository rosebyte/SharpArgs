using System.Collections.Generic;
using System.Linq;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser
{
    internal class ArgumentsParser : IArgumentsParser
    {
        public void ParseArgs(IReadOnlyList<string> args, IParsingHelper helper)
        {
            var arguments = helper.Positions
                .OrderBy(x => x.Key)
                .Select(x => x.Value)
                .Where(x => !helper.Resolved.Contains(x))
                .ToList();
            
            for (var i = 0; i < args.Count; i++)
            {
                if (i >= arguments.Count) break;
                arguments[i].Set(args[i]);
                helper.Resolved.Add(arguments[i]);
            }
        }
    }
}