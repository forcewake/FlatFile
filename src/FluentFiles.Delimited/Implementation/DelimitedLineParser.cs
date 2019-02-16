namespace FluentFiles.Delimited.Implementation
{
    using System;
    using FluentFiles.Core.Base;
    using FluentFiles.Core.Extensions;

    /// <summary>
    /// Converts lines of a delimited file into record instances.
    /// </summary>
    public sealed class DelimitedLineParser : LineParserBase<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>,
                                              IDelimitedLineParser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DelimitedLineParser"/>.
        /// </summary>
        /// <param name="layout">Describes how a file record maps to a type.</param>
        public DelimitedLineParser(IDelimitedLayoutDescriptor layout)
            : base(layout)
        {
        }

        /// <summary>
        /// Maps a line of a delimited file to an instance of <typeparamref name="TRecord" />.
        /// </summary>
        /// <typeparam name="TRecord">The type to map to.</typeparam>
        /// <param name="line">The file line to parse.</param>
        /// <param name="entity">The instance to populate.</param>
        /// <returns>An instance of <typeparamref name="TRecord" /> populated with the parsed and transformed data from <paramref name="line" />.</returns>
        public override TRecord ParseLine<TRecord>(ReadOnlySpan<char> line, TRecord entity)
        {
            var quotesSpan = Layout.Quotes.AsSpan();
            var delimiterSpan = Layout.Delimiter.AsSpan();

            int linePosition = 0;
            int delimiterSize = delimiterSpan.Length;
            var hasQuotes = !quotesSpan.IsEmpty;
            foreach (var field in Layout.Fields)
            {
                int nextDelimiterIndex = -1;
                if (linePosition + delimiterSize <= line.Length)
                {
                    if (hasQuotes)
                    {
                        if (quotesSpan.Equals(line.Slice(linePosition, quotesSpan.Length), StringComparison.OrdinalIgnoreCase))
                        {
                            nextDelimiterIndex = line.IndexOf(quotesSpan, linePosition + 1, StringComparison.OrdinalIgnoreCase);
                            if (line.Length > nextDelimiterIndex)
                            {
                                nextDelimiterIndex = line.IndexOf(delimiterSpan, nextDelimiterIndex, StringComparison.OrdinalIgnoreCase);
                            }
                        }
                    }

                    if (nextDelimiterIndex == -1)
                    {
                        nextDelimiterIndex = line.IndexOf(delimiterSpan, linePosition, StringComparison.OrdinalIgnoreCase);
                    }
                }
                int fieldLength;
                if (nextDelimiterIndex > -1)
                {
                    fieldLength = nextDelimiterIndex - linePosition;
                }
                else
                {
                    fieldLength = line.Length - linePosition;
                }
                var fieldValueFromLine = line.Slice(linePosition, fieldLength);
                var convertedFieldValue = GetFieldValueFromString(field, fieldValueFromLine);
                field.SetValueOf(entity, convertedFieldValue);
                linePosition += fieldLength + (nextDelimiterIndex > -1 ? delimiterSize : 0);
            }
            return entity;
        }

        /// <summary>
        /// Performs delimited file specific string pre-processing, such as removing quotes from around fields when necessary.
        /// </summary>
        /// <param name="field">The field mapping.</param>
        /// <param name="memberValue">The field value.</param>
        /// <returns>A pre-processed field value.</returns>
        protected override ReadOnlySpan<char> PreprocessFieldValue(IDelimitedFieldSettingsContainer field, ReadOnlySpan<char> memberValue)
        {
            var quotesSpan = Layout.Quotes.AsSpan();
            return quotesSpan.IsEmpty ? memberValue : memberValue.Trim(quotesSpan);
        }
    }
}