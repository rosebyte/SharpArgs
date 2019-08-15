using RoseByte.SharpArgs.Attributes;

namespace RoseByte.SharpArgs.Tests.TestObjects
{
    public class BindingTest : IRoute
    {
        [Shortcut('f')]
        public bool Flag { get; set; }
        
        [Shortcut('g')]
        public bool FlagVal { get; set; }
        
        [Order(0)]
        public string First { get; set; }
        
        [Order(1)]
        public string Second { get; set; }
        
        public bool FlagOpt { get; set; }
        public string Option { get; set; }
        public string OptionVal { get; set; }
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}