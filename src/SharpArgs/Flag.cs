using RoseByte.SharpArgs.Internal;

namespace RoseByte.SharpArgs
{
    public class Flag : Argument<bool>
    {
        public sealed override int? Order => null;

        public sealed override bool Initialize(string name, string value) => base.Initialize(name, value);
        public sealed override bool Initialize(char name, string value) => base.Initialize(name, value);
        public sealed override bool Initialize(int order, string value) => false;

        protected Flag() : this(default, default) { }
        
        public Flag(
            string name,
            string description,
            char? shortcut = default,
            bool combinable = true,
            bool defaultValue = false) 
            : base(name, shortcut, description, combinable, defaultValue)
        { }
    }
}