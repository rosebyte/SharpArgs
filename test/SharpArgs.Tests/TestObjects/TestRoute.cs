using SharpArgs.TestObjects;

namespace RoseByte.SharpArgs.Tests.TestObjects
{
    public class TestRoute : IRoute
    {
        public int IntProp { get; set; }
        public bool BoolProp { get; set; }
        public string StringProp { get; set; }
        
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}