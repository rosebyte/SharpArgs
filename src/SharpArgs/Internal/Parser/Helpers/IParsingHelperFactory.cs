using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser.Helpers
{
    public interface IParsingHelperFactory
    {
        IParsingHelper Create(IEnumerable<IProperty> properties);
        IParsingHelper Create(object route);
    }
}