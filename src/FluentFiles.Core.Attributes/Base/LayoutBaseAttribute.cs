namespace FluentFiles.Core.Attributes.Base
{
    using System;

    /// <summary>
    /// Base for an attribute that indicates a type is the mapping target of a file record.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class LayoutBaseAttribute : Attribute
    {
        /// <summary>
        /// Whether a record has a header.
        /// </summary>
        public bool HasHeader { get; set; }
    }
}