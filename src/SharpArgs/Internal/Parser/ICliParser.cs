using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Parser.Options;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public interface ICliParser<in T>
    {
        void Register(IResult<T> result);
        void Parse(IReadOnlyList<string> args, IReadOnlyParsingOptions options);
    }
}