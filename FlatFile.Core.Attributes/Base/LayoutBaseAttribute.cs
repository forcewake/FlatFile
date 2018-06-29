namespace FlatFile.Core.Attributes.Base
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class LayoutBaseAttribute : Attribute
    {
        public bool HasHeader { get; set; }
    }
}