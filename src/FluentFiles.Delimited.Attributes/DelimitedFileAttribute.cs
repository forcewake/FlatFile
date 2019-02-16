namespace FluentFiles.Delimited.Attributes
{
    using FluentFiles.Core.Attributes.Base;
    using System;

    /// <summary>
    /// Configures a type as the mapping target of a delimited file record.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DelimitedFileAttribute : LayoutBaseAttribute
    {
        /// <summary>
        /// The string separating fields of a record.
        /// </summary>
        public string Delimiter { get; set; }

        /// <summary>
        /// The string used to quote fields of a record.
        /// </summary>
        public string Quotes { get; set; }
    }
}