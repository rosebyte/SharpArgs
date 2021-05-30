using System;
using System.ComponentModel;
using System.Linq;

namespace RoseByte.SharpArgs
{
    public abstract class Argument
    {
        public bool Initialized { get; protected set; }
        public virtual string Description { get; }
        public virtual string Name { get; }
        public virtual char? Shortcut { get; }
        public virtual int? Order { get; }
        public virtual bool Combinable { get; }
        public virtual void Validate(){ }

        public virtual bool Initialize(string name, string value)
        {
            return false;
        }

        public virtual bool Initialize(char name, string value)
        {
            return false;
        }

        public virtual bool Initialize(int order, string value)
        {
            return false;
        }
        
        protected Argument() { }

        protected Argument(string name, char? shortcut, string description, bool combinable)
        {
            Name = name;
            Shortcut = shortcut?.ToString().ToLowerInvariant().First();
            Description = description;
            Combinable = combinable;
        }

        protected Argument(int? order, string description)
        {
            Order = order;
            Description = description;
        }
    }
    
    public abstract class Argument<T> : Argument
    {
        public virtual T DefaultValue { get; }
        
        private T _value;
        public T Value => Initialized ? _value : DefaultValue;

        public override bool Initialize(string name, string value)
        {
            if (Initialized || !string.Equals(Name, name, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            
            SetValue(value ?? "true");
            Validate();
            return true;
        }
        
        public override bool Initialize(char name, string value)
        {
            if (Initialized || Shortcut != name)
            {
                return false;
            }
            
            SetValue(value ?? "true");
            Validate();
            return true;
        }
        
        public override bool Initialize(int order, string value)
        {
            if (Initialized || Order != order)
            {
                return false;
            }
            
            SetValue(value);
            Validate();
            return true;
        }

        private void SetValue(string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            var result = converter.ConvertFromString(value);
            _value = (T)result;
            Initialized = true;
        }

        protected Argument() { }

        protected Argument(string name, char? shortcut, string description, bool combinable, T defaultValue)
            : base(name, shortcut, description, combinable)
        {
            DefaultValue = defaultValue;
        }
        
        protected Argument(string name, char? shortcut, string description, bool combinable)
            : base(name, shortcut, description, combinable)
        { }

        protected Argument(int? order, string description, T defaultValue) : base(order, description)
        {
            DefaultValue = defaultValue;
        }
        
        protected Argument(int? order, string description) : base(order, description) { }
    }
}