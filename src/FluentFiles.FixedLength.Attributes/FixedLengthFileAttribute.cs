namespace FluentFiles.FixedLength.Attributes
{
    using FluentFiles.Core.Attributes.Base;
    using System;

    /// <summary>
    /// Configures a type as the mapping target of a fixed-length file record.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FixedLengthFileAttribute : LayoutBaseAttribute
    {
    }
}
