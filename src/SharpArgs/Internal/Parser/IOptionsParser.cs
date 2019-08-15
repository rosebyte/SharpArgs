using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Parser.Options;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public interface IOptionsParser
    {
        void ParseParams(
            IReadOnlyList<string> args, 
            IParsingOptions options,
            IParsingHelper helper);
    }
}