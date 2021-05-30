using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects
{
    public class DummyIntArgument : Argument<int>
    {
        public DummyIntArgument(string name) : base(name, default, string.Empty, default) { }
        public DummyIntArgument(char shortcut) : base(default, shortcut, string.Empty, default) { }
        public DummyIntArgument(int order) : base(order, default) { }
    }
}