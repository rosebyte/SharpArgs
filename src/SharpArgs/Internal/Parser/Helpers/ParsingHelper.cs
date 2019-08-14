using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser.Helpers
{
    public class ParsingHelper : IParsingHelper
    {
        public Dictionary<string, IProperty> Labels { get; }
        public Dictionary<int, IProperty> Positions { get; }
        public Dictionary<char, IProperty> Shortcuts { get; }
        public HashSet<IProperty> Resolved { get; }

        public ParsingHelper(IEnumerable<IProperty> properties)
        {
            Labels = new Dictionary<string, IProperty>();
            Positions = new Dictionary<int, IProperty>();
            Shortcuts = new Dictionary<char, IProperty>();
            Resolved = new HashSet<IProperty>();
            
            foreach (var property in properties)
            {
                if (property.Order.HasValue)
                {
                    Positions.Add(property.Order.Value, property);
                }

                if (property.Shortcut.HasValue)
                {
                    Shortcuts.Add(property.Shortcut.Value, property);
                }
            
                Labels.Add(property.Label, property);
            }
        }
    }
}