using System;
using System.Collections.Generic;
using System.Linq;

namespace RoseByte.SharpArgs.Internal.Parser
{
    internal class UnixArgumentsParser : IArgumentsParser
    {
        public static IArgumentsParser Instance { get; } = new UnixArgumentsParser();
        
        public void ParseArgs(IReadOnlyList<string> args, List<Argument> arguments)
        {
            var positionalMode = false;
            var order = 0;
            for (var i = 0; i < args.Count; i++)
            {
                if (args[i] == "--")
                {
                    positionalMode = true;
                }
                else if (!args[i].StartsWith("-") || positionalMode)
                {
                    ProcesOption(args[i], ++order, arguments);
                }
                else if (args[i].StartsWith("--"))
                {
                    ProcessParameter(args[i].Substring(2), GetValue(args, ref i), arguments);
                }
                else if (args[i].StartsWith("-") && args[i].Length == 2)
                {
                    ProcessParameter(args[i].ToLowerInvariant().Last(), GetValue(args, ref i), arguments);
                }
                else
                {
                    foreach (var flag in args[i].ToLowerInvariant().Skip(1))
                    {
                        ProcessParameter(flag, null, arguments.Where(x => x.Combinable));
                    }
                }
            }
        }

        private string GetValue(IReadOnlyList<string> args, ref int i)
        {
            return i < args.Count - 2 && !args[i + 1].StartsWith("-") ? args[++i] : null;
        }

        private void ProcessParameter(string name, string value, IEnumerable<Argument> arguments)
        {
            foreach (var argument in arguments)
            {
                if (argument.Initialize(name, value))
                {
                    return;
                }
            }

            throw new Exception();
        }

        private void ProcessParameter(char name, string value, IEnumerable<Argument> arguments)
        {
            foreach (var argument in arguments)
            {
                if (argument.Initialize(name, value))
                {
                    return;
                }
            }

            throw new Exception();
        }

        private void ProcesOption(string value, int order, IEnumerable<Argument> arguments)
        {
            foreach (var argument in arguments)
            {
                if (argument.Initialize(order, value))
                {
                    return;
                }
            }

            throw new Exception();
        }
    }
}