using System;
using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects
{
    public class DummyBoolArgument : Argument<bool>
    {
        public DummyBoolArgument(string name) : base(name, default, string.Empty, default) { }
        
        public Action ValidationAction;
        public override void Validate()
        {
            ValidationAction?.Invoke();
        }
    }
}