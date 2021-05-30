using System.Collections.Generic;

namespace RoseByte.SharpArgs.Parsers
{
    public interface IArgumentsParser
    {
        void ParseArgs(IReadOnlyList<string> args, List<Argument> arguments);
    }
}