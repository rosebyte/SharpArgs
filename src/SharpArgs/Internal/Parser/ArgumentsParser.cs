using System.Collections.Generic;
using System.Linq;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public class ArgumentsParser
    {
        public void ParseArgs(
            IReadOnlyList<string> args, 
            Dictionary<int, Property> positions, 
            HashSet<Property> resolved)
        {
            var arguments = positions
                .OrderBy(x => x.Key)
                .Select(x => x.Value)
                .Where(x => !resolved.Contains(x))
                .ToList();
            
            for (var i = 0; i < args.Count; i++)
            {
                if (i >= arguments.Count) break;
                arguments[i].Set(args[i]);
                resolved.Add(arguments[i]);
            }
        }
    }
}