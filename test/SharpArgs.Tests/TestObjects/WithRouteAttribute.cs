using RoseByte.SharpArgs.Attributes;

namespace RoseByte.SharpArgs.Tests.TestObjects
{
    [Route("first", "second")]
    public class WithRouteAttribute : ISecondRoute
    {
        
    }
}