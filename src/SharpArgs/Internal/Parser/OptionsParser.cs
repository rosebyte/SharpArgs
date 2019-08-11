using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Exceptions;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public class OptionsParser
    {
        public void ParseParams(
            IReadOnlyList<string> args, 
            IReadOnlyParsingOptions options,
            Dictionary<string, Property> labels,
            Dictionary<char, Property> shortcuts,
            HashSet<Property> resolved)
        {
            for (var i = 0; i < args.Count; i++)
            {
                var part = new Part(args[i], options);
                if (!part.IsOption && !part.IsFlag)
                {
                    throw new SharpArgsException($"Unknown part: '{part.Content}'.");
                }

                var next = i + 1 < args.Count 
                    ? new Part(args[i + 1], options)
                    : null;

                if (next != null && (next.IsOption || next.IsFlag))
                {
                    next = null;
                }

                if (next != null)
                {
                    i++;
                }

                if (part.IsOption && ParseOption(part, next, options, labels, resolved))
                {
                    continue;
                }
                
                if (part.IsFlag && ParseFlag(part, next, options, shortcuts, resolved))
                {
                    continue;
                }
                
                throw new SharpArgsException($"Unknown part: '{part.Content}'.");
            }
        }
        
        private bool ParseOption(Part part, Part next, IReadOnlyParsingOptions options, 
            Dictionary<string, Property> labels, HashSet<Property> resolved)
        {
            if (!labels.TryGetValue(part.Label, out var named))
            {
                return false;
            }
            
            SetValue(part, next, named, options, resolved);
            
            return true;
        }

        private void SetValue(Part part, Part next, Property property, IReadOnlyParsingOptions options, 
            HashSet<Property> resolved)
        {
            if (part.Value != null)
            {
                if (next != null)
                {
                    throw new SharpArgsException($"Unknown part: '{next.Content}'.");
                }
                    
                property.Set(part.Value);
                resolved.Add(property);
            }
            else if (!options.Delimiters.Contains(' ') || next == null)
            {
                if (property.Type == typeof(bool))
                {
                    property.Set(true);
                    resolved.Add(property);
                }
            }
            else
            {
                property.Set(next.Content);
                resolved.Add(property);
            }
        }

        private bool ParseFlag(Part part, Part next, IReadOnlyParsingOptions options, 
            Dictionary<char, Property> shortcuts, HashSet<Property> resolved)
        {
            if (part.Label.Length > 1 && !options.CanCombineFlags)
            {
                throw new SharpArgsException($"Can't parse multiple combined flags: '{part.Content}'");
            }
            
            foreach (var label in part.Label)
            {
                if (!shortcuts.TryGetValue(label, out var named))
                {
                    throw new SharpArgsException($"Unknown key: '{label}' in '{part.Content}'.");
                }
                            
                SetValue(part, next, named, options, resolved);
                resolved.Add(named);
            }
            
            return true;
        }
    }
}