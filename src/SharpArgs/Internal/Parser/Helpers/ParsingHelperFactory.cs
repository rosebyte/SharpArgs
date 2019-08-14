using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser.Helpers
{
    public class ParsingHelperFactory : IParsingHelperFactory
    {
        public IParsingHelper Create(IEnumerable<IProperty> properties)
        {
            return new ParsingHelper(properties);
        }
    }
}