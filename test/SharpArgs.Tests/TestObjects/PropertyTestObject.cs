using System;
using RoseByte.SharpArgs.Attributes;

namespace RoseByte.SharpArgs.Tests.TestObjects
{
    public class PropertyTestObject
    {
        public string StrProp { get; set; }
        public int IntProp { get; set; }
        public bool BoolProp { get; set; }
        public DateTime DateTimeProp { get; set; }
        
        [Ignore]
        public string IgnoredProp { get; set; }
        
        [Shortcut('s')]
        public string ShortcutProp { get; set; }
        
        [Label("testme")]
        public string LabeledProp { get; set; }
        
        [Order(4)]
        public string OrderedProp { get; set; }
        
        [Description("This is a short description.")]
        public string DescribedProp { get; set; }
    }
}