namespace FluentFiles.FixedLength.Attributes
{
    using System;

    /// <summary>
    /// Configures a span of ignored characters in a fixed-length file record.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class IgnoreFixedLengthFieldAttribute : Attribute
    {
        /// <summary>
        /// Where in a line the ignored section begins.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// The length of the section to ignore.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Initializes a new <see cref="IgnoreFixedLengthFieldAttribute"/>.
        /// </summary>
        /// <param name="index">Where in a line the ignored section begins.</param>
        /// <param name="length">The length of the section to ignore.</param>
        public IgnoreFixedLengthFieldAttribute(int index, int length)
        {
            Index = index;
            Length = length;
        }
    }
}