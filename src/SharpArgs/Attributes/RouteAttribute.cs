using System;

namespace RoseByte.SharpArgs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteAttribute : Attribute
    {
        public string[] Labels { get; }
        public bool Default { get; set; }

        public RouteAttribute(params string[] labels) => Labels = labels;
    }
}