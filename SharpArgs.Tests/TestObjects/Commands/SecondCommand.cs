using System;
using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Commands
{
    public class SecondCommand : Command
    {
        private readonly Action _action;
        public override string Name => "second";
        
        protected override void Execute()
        {
            _action?.Invoke();
        }

        public SecondCommand(Action action)
        {
            _action = action;
        }
    }
}