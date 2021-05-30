using RoseByte.SharpArgs;
using SharpArgs.Tests.TestObjects.Arguments.Flags;

namespace SharpArgs.Tests.TestObjects.Commands
{
    public class ArgumentsCommand : Command
    {
        public Flag InstantiatedFlagProperty { get; } = new Flag("iflagprop", "instantiated flag description");
        public Flag InstantiatedFlagField = new Flag("iflagfield", "instantiated flag description");
        public FirstFlag FlagProperty { get; private set; }
        public FirstFlag FlagField;
    }
}