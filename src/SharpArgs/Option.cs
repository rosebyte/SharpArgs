namespace RoseByte.SharpArgs
{
    public class Option<T> : Argument<T>
    {
        public sealed override string Name => default;
        public sealed override char? Shortcut => default;
        public sealed override bool Combinable => false;

        public sealed override bool Initialize(string name, string value) => false;
        public sealed override bool Initialize(char name, string value) => false;
        public sealed override bool Initialize(int order, string value) => base.Initialize(order, value);
        
        protected Option() { }

        public Option(int order, string description) : base(order, description) { }
        public Option(int order, string description, T defaultValue) : base(order, description, defaultValue) { }
    }
}