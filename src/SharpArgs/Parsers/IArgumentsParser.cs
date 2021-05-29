using System.Collections.Generic;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public interface IArgumentsParser
    {
        void ParseArgs(IReadOnlyList<string> args, List<Argument> arguments);
    }
}