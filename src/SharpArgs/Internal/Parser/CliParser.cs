using System.Collections.Generic;
using System.Linq;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public class CliParser : ICliParser
    {
        private readonly Dictionary<string, Property> _labels = new Dictionary<string, Property>();
        private readonly Dictionary<int, Property> _positions = new Dictionary<int, Property>();
        private readonly Dictionary<char, Property> _shortcuts = new Dictionary<char, Property>();
        private readonly HashSet<Property> _resolved = new HashSet<Property>();

        public ICliParser Scan(IEnumerable<Property> properties)
        {
            foreach (var prop in properties)
            {
                Register(prop);
            }

            return this;
        }
        
        private void Register(Property property)
        {
            if (property.Order.HasValue)
            {
                _positions.Add(property.Order.Value, property);
            }

            if (property.Shortcut.HasValue)
            {
                _shortcuts.Add(property.Shortcut.Value, property);
            }
            
            _labels.Add(property.Label, property);
        }

        public void Parse(IReadOnlyList<string> args, IReadOnlyParsingOptions options)
        {
            var arguments = args
                .TakeWhile(arg => !arg.StartsWith(options.OptionPrefix) && !arg.StartsWith(options.FlagPrefix))
                .ToList();

            new OptionsParser().ParseParams(args.Skip(arguments.Count).ToList(), options, _labels, _shortcuts, 
                _resolved);
            new ArgumentsParser().ParseArgs(arguments, _positions, _resolved);
        }
    }
}