using System.Collections.Generic;
using RoseByte.SharpArgs.Exceptions;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Parser.Options;
using RoseByte.SharpArgs.Internal.Parser.Parts;
using RoseByte.SharpArgs.Internal.Properties;

namespace RoseByte.SharpArgs.Internal.Parser
{
    public class OptionsParser : IOptionsParser
    {
        public void ParseParams(IReadOnlyList<string> args, IParsingOptions options, IParsingHelper helper)
        {
            for (var i = 0; i < args.Count; i++)
            {
                var part = new Part(args[i], options);
                if (!part.IsOption && !part.IsFlag)
                {
                    throw new SharpArgsException($"Unknown part: '{part.Content}'.");
                }

                var next = i + 1 < args.Count ? new Part(args[i + 1], options) : null;

                if (next != null && (next.IsOption || next.IsFlag))
                {
                    next = null;
                }

                if (next != null)
                {
                    i++;
                }

                if (part.IsOption && ParseOption(part, next, options, helper.Labels, helper.Resolved))
                {
                    continue;
                }
                
                if (part.IsFlag && ParseFlag(part, next, options, helper.Shortcuts, helper.Resolved))
                {
                    continue;
                }
                
                throw new SharpArgsException($"Unknown part: '{part.Content}'.");
            }
        }
        
        private bool ParseOption(Part part, Part next, IParsingOptions options, 
            Dictionary<string, IProperty> labels, HashSet<IProperty> resolved)
        {
            if (!labels.TryGetValue(part.Label.ToLower(), out var named))
            {
                return false;
            }
            
            SetValue(part, next, named, options, resolved);
            return true;
        }

        private void SetValue(Part part, Part next, IProperty property, IParsingOptions options, 
            HashSet<IProperty> resolved)
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

        private bool ParseFlag(Part part, Part next, IParsingOptions options, 
            Dictionary<char, IProperty> shortcuts, HashSet<IProperty> resolved)
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