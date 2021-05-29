using RoseByte.SharpArgs.Options;

namespace SharpArgs.Tests.TestObjects.Arguments
{
    public class ThirdBoolOption : Option<bool>
    {
        public override bool DefaultValue => true;
        public override int? Order => 3;
        public override string Description => "This is the third option";
    }
}