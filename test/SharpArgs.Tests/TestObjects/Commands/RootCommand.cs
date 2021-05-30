using System;
using System.Collections.Generic;
using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Commands
{
    public class RootCommand : Command
    {
        private readonly Action<string> _action;
        
        public override IEnumerable<Command> Commands
        {
            get
            {
                yield return new FirstCommand(() => _action?.Invoke("first"));
                yield return new SecondCommand(() => _action?.Invoke("second"));
            }
        }

        public RootCommand(Action<string> action)
        {
            _action = action;
        }
    }
}