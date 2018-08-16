namespace FlatFile.Core.Attributes.Entities
{
    using System;
    using System.Reflection;

    internal class PropertyDescription
    {
        public PropertyInfo Property { get; set; }
        public Attribute[] Attributes { get; set; }
    }
}
