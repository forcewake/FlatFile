namespace FluentFiles.Delimited.Attributes
{
    using FluentFiles.Core.Attributes.Base;
    using System;

    /// <summary>
    /// Configures a member as the mapping target of a delimited field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DelimitedFieldAttribute : FieldSettingsBaseAttribute, IDelimitedFieldSettings
    {
        /// <summary>
        /// The name to use when writing a field's header.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new <see cref="DelimitedFieldAttribute"/>.
        /// </summary>
        /// <param name="index">Where a field appears in a line.</param>
        public DelimitedFieldAttribute(int index)
            : base(index)
        {
        }
    }
}
