namespace FluentFiles.FixedLength.Attributes
{
    using System;
    using FluentFiles.Core.Attributes.Base;

    /// <summary>
    /// Configures a member as the mapping target of a fixed-length field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class FixedLengthFieldAttribute : FieldSettingsBaseAttribute, IFixedFieldSettings
    {
        /// <summary>
        /// The length of a field.
        /// </summary>
        public int Length { get; protected set; }

        /// <summary>
        /// The type of padding a field has.
        /// </summary>
        public Padding Padding { get; set; }

        /// <summary>
        /// Whether a field has padding to the left or right.
        /// </summary>
        public bool PadLeft => Padding == Padding.Left;

        /// <summary>
        /// The character used to pad a field.
        /// </summary>
        public char PaddingChar { get; set; }

        /// <summary>
        /// Whether a field's contents should be truncated if it exceeds the configured length when writing to a file.
        /// </summary>
        public bool TruncateIfExceedFieldLength { get; set; }

        Func<string, string> IFixedFieldSettings.StringNormalizer { get; }

        /// <summary>
        /// A condition that, when true, indicates that the character at the given index
        /// should not be included in the field's value. Application of the predicate will stop at
        /// the first false evaluation.
        /// </summary>
        Func<char, int, bool> IFixedFieldSettings.SkipWhile { get; }

        /// <summary>
        /// A condition that, when true, indicates that the character at the given index
        /// should be included in the field's value. Application of the predicate will stop at
        /// the first true evaluation.
        /// </summary>
        Func<char, int, bool> IFixedFieldSettings.TakeUntil { get; }

        /// <summary>
        /// Initializes a new <see cref="FixedLengthFieldAttribute"/>.
        /// </summary>
        /// <param name="index">Where a field appears in a line.</param>
        /// <param name="length">The length of a field.</param>
        /// <param name="truncateIfExceed">Whether a field's contents should be truncated if it exceeds the configured length when writing to a file.</param>
        public FixedLengthFieldAttribute(int index, int length, bool truncateIfExceed = false)
            : base(index)
        {
            Padding = Padding.Left;
            TruncateIfExceedFieldLength = truncateIfExceed;
            Length = length;
        }
    }
}