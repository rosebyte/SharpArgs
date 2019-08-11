using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public interface ICliParser
    {
        ICliParser Scan(IEnumerable<Property> properties);
        void Parse(IReadOnlyList<string> args, IReadOnlyParsingOptions options);
    }
}