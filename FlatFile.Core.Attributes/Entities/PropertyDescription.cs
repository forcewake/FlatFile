namespace FlatFile.Core.Attributes.Entities
{
    using System;
    using System.Reflection;

    public class PropertyDescription
    {
        public PropertyInfo Property { get; set; }
        public Attribute[] Attributes { get; set; }
    }
}
