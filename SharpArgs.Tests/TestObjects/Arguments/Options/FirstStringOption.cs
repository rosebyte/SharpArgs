using RoseByte.SharpArgs.Options;

namespace SharpArgs.Tests.TestObjects.Arguments
{
    public class FirstStringOption : Option<string>
    {
        public override string DefaultValue => "FirstDefaultValue";
        public override int? Order => 1;
        public override string Description => "This is the first option";
    }
}