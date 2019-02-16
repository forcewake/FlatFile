namespace FluentFiles.Delimited.Implementation
{
    using System.Linq;
    using System.Text;
    using FluentFiles.Core.Base;

    /// <summary>
    /// Converts record instances into formatted lines of a delimited file.
    /// </summary>
    public class DelimitedLineBuilder : LineBuilderBase<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>, 
                                        IDelimitedLineBuilder
    {
        private readonly StringBuilder _lineBuilder;

        /// <summary>
        /// Initializes a new instance of <see cref="DelimitedLineBuilder"/>.
        /// </summary>
        /// <param name="layout">Describes how a type maps to a file record.</param>
        public DelimitedLineBuilder(IDelimitedLayoutDescriptor layout)
            : base(layout)
        {
            _lineBuilder = new StringBuilder(Layout.Fields.Count() * 5);
        }

        /// <summary>
        /// Maps an instance of <typeparamref name="TRecord" /> to a line in a delimited file.
        /// </summary>
        /// <typeparam name="TRecord">The type to map.</typeparam>
        /// <param name="entity">The instance to map.</param>
        /// <returns>The formatted value of the data in <paramref name="entity" />.</returns>
        public override string BuildLine<TRecord>(TRecord entity)
        {
            _lineBuilder.Clear();
            foreach (var field in Layout.Fields)
            {
                _lineBuilder.Append(_lineBuilder.Length > 0 ? Layout.Delimiter : string.Empty)
                            .Append(GetStringValueFromField(field, field.GetValueOf(entity)));
            }
            return _lineBuilder.ToString();
        }

        /// <summary>
        /// Performs delimited file specific string post-processing, such as adding quotes around fields when necessary.
        /// </summary>
        /// <param name="field">The field mapping.</param>
        /// <param name="value">The already formatted value to process.</param>
        /// <returns>A post-processed field value.</returns>
        protected override string PostprocessFieldValue(IDelimitedFieldSettingsContainer field, string value)
        {
            var quotes = Layout.Quotes;
            if (!string.IsNullOrEmpty(quotes))
            {
                value = string.Format("{0}{1}{0}", quotes, value);
            }
            return value;
        }
    }
}