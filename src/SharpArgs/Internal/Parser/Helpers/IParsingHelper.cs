using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser.Helpers
{
    public interface IParsingHelper
    {
        Dictionary<string, IProperty> Labels { get; }
        Dictionary<int, IProperty> Positions { get; }
        Dictionary<char, IProperty> Shortcuts { get; }
        HashSet<IProperty> Resolved { get; }
    }
}