using System;
using System.Threading.Tasks;
using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Commands
{
    public class AsyncCommand : Command
    {
        private readonly Func<Task> _action;
        public override string Name => "first";
        
        protected override Task ExecuteAsync()
        {
            return _action?.Invoke();
        }

        public AsyncCommand(Func<Task> action)
        {
            _action = action;
        }
    }
}