using System.Collections.Generic;
using System.Linq;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public class CliParser
    {
        private readonly Dictionary<string, Property> _labels = new Dictionary<string, Property>();
        private readonly Dictionary<int, Property> _positions = new Dictionary<int, Property>();
        private readonly Dictionary<char, Property> _shortcuts = new Dictionary<char, Property>();
        private readonly HashSet<Property> _resolved = new HashSet<Property>();
        private readonly IReadOnlyParsingOptions _options;

        public CliParser(IEnumerable<Property> properties, IReadOnlyParsingOptions options)
        {
            _options = options;
            foreach (var prop in properties.Where(x => !x.Ignore))
            {
                Register(prop);
            }
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

        public void Parse(IReadOnlyList<string> args)
        {
            var arguments = args
                .TakeWhile(arg => !arg.StartsWith(_options.OptionPrefix) && !arg.StartsWith(_options.FlagPrefix))
                .ToList();

            new OptionsParser().ParseParams(args.Skip(arguments.Count).ToList(), _options, _labels, _shortcuts, 
                _resolved);
            new ArgumentsParser().ParseArgs(arguments, _positions, _resolved);
        }
        
        
    }
}