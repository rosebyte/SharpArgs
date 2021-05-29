using RoseByte.SharpArgs.Options;

namespace SharpArgs.Tests.TestObjects.Arguments
{
    public class SecondIntOption : Option<int>
    {
        public override int DefaultValue => 5;
        public override int? Order => 2;
        public override string Description => "This is the second option";
    }
}