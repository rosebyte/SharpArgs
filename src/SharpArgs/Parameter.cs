using System;

namespace RoseByte.SharpArgs
{
    public class Parameter<T>: Argument<T>
    {
        public sealed override int? Order => null;
        public sealed override bool Combinable => false;
        public sealed override bool Initialize(string name, string value) => base.Initialize(name, value);
        public sealed override bool Initialize(char name, string value) => base.Initialize(name, value);
        public sealed override bool Initialize(int order, string value) => false;

        protected Parameter() { }
               
        public Parameter(string name, string description, char? shortcut = default, bool combinable = false, T defaultValue = default) 
            : base(name, shortcut, description, combinable, defaultValue)
        { }
    }
}