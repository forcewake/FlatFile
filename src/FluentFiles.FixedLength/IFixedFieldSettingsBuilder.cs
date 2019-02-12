namespace FluentFiles.FixedLength
{
    using System;
    using FluentFiles.Core;

    /// <summary>
    /// Configures a fixed-length file field.
    /// </summary>
    public interface IFixedFieldSettingsBuilder : IFieldSettingsBuilder<IFixedFieldSettingsBuilder, IFixedFieldSettingsContainer>
    {
        /// <summary>
        /// Specifies the expected length of a field.
        /// </summary>
        /// <param name="length">The length of the field.</param>
        IFixedFieldSettingsBuilder WithLength(int length);

        /// <summary>
        /// Specifies that a field begins with padding that should be removed.
        /// </summary>
        /// <param name="paddingChar">The padding character.</param>
        IFixedFieldSettingsBuilder WithLeftPadding(char paddingChar);

        /// <summary>
        /// Specifies that a field ends with padding that should be removed.
        /// </summary>
        /// <param name="paddingChar">The padding character.</param>
        IFixedFieldSettingsBuilder WithRightPadding(char paddingChar);

        /// <summary>
        /// Provides a condition that, when true, indicates that the character at the given index
        /// should not be included in the field's value. Application of the predicate will stop at
        /// the first false evaluation.
        /// </summary>
        /// <param name="predicate">The predicate to apply to each character.</param>
        IFixedFieldSettingsBuilder SkipWhile(Func<char, int, bool> predicate);

        /// <summary>
        /// Provides a condition that, when true, indicates that the character at the given index
        /// should be included in the field's value. Application of the predicate will stop at
        /// the first true evaluation.
        /// </summary>
        /// <param name="predicate">The predicate to apply to each character.</param>
        IFixedFieldSettingsBuilder TakeUntil(Func<char, int, bool> predicate);

        /// <summary>
        /// Determines whether a field's contents should be truncated if it exceeds the configured length when writing to a file.
        /// </summary>
        /// <returns></returns>
        IFixedFieldSettingsBuilder TruncateFieldContentIfExceedLength();


        IFixedFieldSettingsBuilder WithStringNormalizer(Func<string, string> stringNormalizer);
    }
}