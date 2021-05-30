using System;
using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Commands
{
    public class FirstCommand : Command
    {
        private readonly Action _action;
        public override string Name => "first";
        
        protected override void Execute()
        {
            _action?.Invoke();
        }

        public FirstCommand(Action action)
        {
            _action = action;
        }
    }
}