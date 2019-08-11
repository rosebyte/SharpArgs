using System;
using System.ComponentModel;
using System.Reflection;
using RoseByte.SharpArgs.Attributes;
using RoseByte.SharpArgs.Internal.Extensions;
using DescriptionAttribute = RoseByte.SharpArgs.Attributes.DescriptionAttribute;

namespace RoseByte.SharpArgs.Internal.Properties
{
    internal class Property
    {
        public int? Order => Info.GetAttribute<OrderAttribute>()?.Order;
        public char? Shortcut => Info.GetAttribute<ShortcutAttribute>()?.Shortcut;
        public string Label => Info.GetAttribute<LabelAttribute>()?.Label ?? Name;
        public string Description => Info.GetAttribute<DescriptionAttribute>()?.Description;
        public bool Ignore => Info.HasAttribute<IgnoreAttribute>();
        public string Name => Info.Name.ToLower();
        public Type Type => Info.PropertyType;
        
        private object Instance { get; }
        private PropertyInfo Info { get; }
        
        public Property(object instance, PropertyInfo info)
        {
            Instance = instance;
            Info = info;
        }

        public void Set(bool value) => Info.SetValue(Instance, value);
        
        public void Set(string value)
        {
            if (Type == typeof(string))
            {
                Info.SetValue(Instance, value);
            }
            
            var converter = TypeDescriptor.GetConverter(Type);
            var result = converter.ConvertFromString(value);
            Info.SetValue(Instance, result);
        }
    }
}