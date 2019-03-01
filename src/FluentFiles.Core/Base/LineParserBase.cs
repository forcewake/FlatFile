namespace FluentFiles.Core.Base
{
    using System;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;

    /// <summary>
    /// Base class for converting lines of a file to records.
    /// </summary>
    /// <typeparam name="TLayoutDescriptor">The type that provides a mapping from a file record to a type.</typeparam>
    /// <typeparam name="TFieldSettings">The type of individual field mapping within a <typeparamref name="TLayoutDescriptor"/>.</typeparam>
    public abstract class LineParserBase<TLayoutDescriptor, TFieldSettings> : ILineParser
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LineParserBase{TLayoutDescriptor, TFieldSettings}"/>.
        /// </summary>
        /// <param name="layout">Describes how a file record maps to a type.</param>
        protected LineParserBase(TLayoutDescriptor layout)
        {
            Layout = layout;
        }

        /// <summary>
        /// Describes how a file record maps to a type.
        /// </summary>
        protected TLayoutDescriptor Layout { get; }

        /// <summary>
        /// Maps a line of a file to an instance of <typeparamref name="TRecord"/>.
        /// </summary>
        /// <typeparam name="TRecord">The type to map to.</typeparam>
        /// <param name="line">The file line to parse.</param>
        /// <param name="entity">The instance to populate.</param>
        /// <returns>An instance of <typeparamref name="TRecord"/> populated with the parsed and transformed data from <paramref name="line"/>.</returns>
        public abstract TRecord ParseLine<TRecord>(ReadOnlySpan<char> line, TRecord entity) where TRecord : new();

        /// <summary>
        /// Extracts and transforms the value of a field.
        /// </summary>
        /// <param name="fieldSettings">The field mapping.</param>
        /// <param name="memberValue">The field value.</param>
        /// <returns>The parsed value of the field.</returns>
        protected virtual object GetFieldValueFromString(TFieldSettings fieldSettings, ReadOnlySpan<char> memberValue)
        {
            if (fieldSettings.IsNullable && memberValue.Trim('"').Equals(fieldSettings.NullValue.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var preprocessed = PreprocessFieldValue(fieldSettings, memberValue);

            var value = ParseField(fieldSettings, preprocessed);
            return value;
        }

        /// <summary>
        /// Performs pre-parsing, file-type specific, field string processing.
        /// </summary>
        /// <param name="field">The field mapping.</param>
        /// <param name="memberValue">The field value.</param>
        /// <returns>A pre-processed field value.</returns>
        protected virtual ReadOnlySpan<char> PreprocessFieldValue(TFieldSettings field, ReadOnlySpan<char> memberValue) => memberValue;

        private static object ParseField(TFieldSettings field, ReadOnlySpan<char> source)
        {
            var converter = field.Converter;
            if (converter != null && converter.CanParse(field.Type.Unwrap()))
                return converter.Parse(new FieldParsingContext(source, field.Member, field.Type));

            return field.Type?.GetDefaultValue();
        }
    }
}