using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Parser.Helpers;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public interface IArgumentsParser
    {
        void ParseArgs(IReadOnlyList<string> args, IParsingHelper helper);
    }
}