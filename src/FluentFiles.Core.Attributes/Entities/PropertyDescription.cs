namespace FluentFiles.Core.Attributes.Entities
{
    using System;
    using System.Reflection;

    internal class PropertyDescription
    {
        public PropertyDescription(PropertyInfo property, Attribute[] attributes)
        {
            Property = property;
            Attributes = attributes;
        }

        public PropertyInfo Property { get; }
        public Attribute[] Attributes { get; }
    }
}
