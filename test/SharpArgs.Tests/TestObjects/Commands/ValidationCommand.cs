using System;
using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Commands
{
    public class ValidationCommand : Command
    {
        private readonly Action _action;
        
        protected override void Validate()
        {
            _action?.Invoke();
        }

        public ValidationCommand(Action action)
        {
            _action = action;
        }
    }
}