namespace FluentFiles.FixedLength
{
    using System;
    using FluentFiles.Core;

    /// <summary>
    /// A fixed-length field mapping configuration.
    /// </summary>
    public interface IFixedFieldSettings : IFieldSettings
    {
        /// <summary>
        /// The length of a field.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Whether a field has padding to the left or right.
        /// </summary>
        bool PadLeft { get; }

        /// <summary>
        /// The character used to pad a field.
        /// </summary>
        char PaddingChar { get; }

        /// <summary>
        /// A condition that, when true, indicates that the character at the given index
        /// should not be included in the field's value. Application of the predicate will stop at
        /// the first false evaluation.
        /// </summary>
        Func<char, int, bool> SkipWhile { get; }

        /// <summary>
        /// A condition that, when true, indicates that the character at the given index
        /// should be included in the field's value. Application of the predicate will stop at
        /// the first true evaluation.
        /// </summary>
        Func<char, int, bool> TakeUntil { get; }

        /// <summary>
        /// Whether a field's contents should be truncated if it exceeds the configured length when writing to a file.
        /// </summary>
        bool TruncateIfExceedFieldLength { get; }

        /// <summary>
        /// A function that transforms the values of an object's members after they have been converted to a string, but
        /// before they have been written to a line in a file.
        /// </summary>
        Func<string, string> StringNormalizer { get; }
    }

    /// <summary>
    /// Extends <see cref="IFixedFieldSettings"/> with functionality and data related to its storage in a class property.
    /// See <see cref="IFieldSettingsContainer"/>.
    /// </summary>
    public interface IFixedFieldSettingsContainer : IFixedFieldSettings, IFieldSettingsContainer
    {
    }
}