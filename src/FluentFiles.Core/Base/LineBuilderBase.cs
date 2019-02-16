namespace FluentFiles.Core.Base
{
    using FluentFiles.Core.Conversion;

    /// <summary>
    /// Base class for converting records to lines of a file.
    /// </summary>
    /// <typeparam name="TLayoutDescriptor">The type that provides a mapping from a type to a file record.</typeparam>
    /// <typeparam name="TFieldSettings">The type of individual field mapping within a <typeparamref name="TLayoutDescriptor"/>.</typeparam>
    public abstract class LineBuilderBase<TLayoutDescriptor, TFieldSettings> : ILineBuilder
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LineBuilderBase{TLayoutDescriptor, TFieldSettings}"/>.
        /// </summary>
        /// <param name="layout">Describes how a type maps to a file record.</param>
        protected LineBuilderBase(TLayoutDescriptor layout)
        {
            Layout = layout;
        }

        /// <summary>
        /// Describes how a type maps to a file record.
        /// </summary>
        protected TLayoutDescriptor Layout { get; }

        /// <summary>
        /// Maps an instance of <typeparamref name="TRecord"/> to a line in a file.
        /// </summary>
        /// <typeparam name="TRecord">The type to map.</typeparam>
        /// <param name="entity">The instance to map.</param>
        /// <returns>The formatted value of the data in <paramref name="entity"/>.</returns>
        public abstract string BuildLine<TRecord>(TRecord entity);

        /// <summary>
        /// Formats a value for writing to a file field.
        /// </summary>
        /// <param name="field">The field mapping.</param>
        /// <param name="fieldValue">The value to format.</param>
        /// <returns>The formatted field value.</returns>
        protected virtual string GetStringValueFromField(TFieldSettings field, object fieldValue)
        {
            string lineValue = fieldValue != null
                ? ConvertToString(field, fieldValue)
                : field.NullValue ?? string.Empty;

            lineValue = PostprocessFieldValue(field, lineValue);

            return lineValue;
        }

        /// <summary>
        /// Performs post-formatting, file-type specific, field string processing.
        /// </summary>
        /// <param name="field">The field mapping.</param>
        /// <param name="value">The already formatted value to process.</param>
        /// <returns>A post-processed field value.</returns>
        protected virtual string PostprocessFieldValue(TFieldSettings field, string value) => value;

        private static string ConvertToString(TFieldSettings field, object fieldValue)
        {
            var converter = field.Converter;
            if (converter != null && converter.CanConvert(from: field.Type, to: typeof(string)))
                return converter.ConvertToString(new FieldSerializationContext(fieldValue, field.PropertyInfo));

            return fieldValue.ToString();
        }
    }
}