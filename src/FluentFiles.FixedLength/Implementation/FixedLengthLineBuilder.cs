namespace FluentFiles.FixedLength.Implementation
{
    using System.Linq;
    using System.Text;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    /// <summary>
    /// Converts record instances into formatted lines of a fixed-length file.
    /// </summary>
    public class FixedLengthLineBuilder : LineBuilderBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
                                          IFixedLengthLineBuilder
    {
        private readonly StringBuilder _lineBuilder;

        /// <summary>
        /// Initializes a new instance of <see cref="FixedLengthLineBuilder"/>.
        /// </summary>
        /// <param name="layout">Describes how a type maps to a file record.</param>
        public FixedLengthLineBuilder(IFixedLengthLayoutDescriptor layout)
            : base(layout)
        {
            _lineBuilder = new StringBuilder(layout.Fields.Sum(f => f.Length));
        }

        /// <summary>
        /// Maps an instance of <typeparamref name="TRecord" /> to a line in a fixed-length file.
        /// </summary>
        /// <typeparam name="TRecord">The type to map.</typeparam>
        /// <param name="entity">The instance to map.</param>
        /// <returns>The formatted value of the data in <paramref name="entity" />.</returns>
        public override string BuildLine<TRecord>(TRecord entity)
        {
            _lineBuilder.Clear();
            foreach (var field in Layout.Fields)
                _lineBuilder.Append(GetStringValueFromField(field, field.GetValueOf(entity)));

            return _lineBuilder.ToString();
        }

        /// <summary>
        /// Performs fixed-length file specific string post-processing, such as adding padding to a field when necessary.
        /// </summary>
        /// <param name="field">The field mapping.</param>
        /// <param name="value">The already formatted value to process.</param>
        /// <returns>A post-processed field value.</returns>
        protected override string PostprocessFieldValue(IFixedFieldSettingsContainer field, string value)
        {
            if (field.StringNormalizer != null)
            {
                value = field.StringNormalizer(value);
            }

            if (value.Length >= field.Length)
            {
                return field.TruncateIfExceedFieldLength ? value.Substring(0, field.Length) : value;
            }

            value = field.PadLeft
                ? value.PadLeft(field.Length, field.PaddingChar)
                : value.PadRight(field.Length, field.PaddingChar);

            return value;
        }
    }
}