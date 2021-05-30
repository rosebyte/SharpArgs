using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects
{
    public class DummyDecimalArgument : Argument<decimal>
    {
        public DummyDecimalArgument(string name) : base(name, default, string.Empty, default) { }

    }
}