namespace FluentFiles.Delimited.Implementation
{
    using System;
    using FluentFiles.Core.Base;
    using FluentFiles.Core.Extensions;

    public sealed class DelimitedLineParser :
        LineParserBase<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>,
        IDelimitedLineParser
    {
        public DelimitedLineParser(IDelimitedLayoutDescriptor layout)
            : base(layout)
        {
        }

        public override TEntity ParseLine<TEntity>(ReadOnlySpan<char> line, TEntity entity)
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

        protected override ReadOnlySpan<char> PreprocessFieldValue(IDelimitedFieldSettingsContainer field, ReadOnlySpan<char> memberValue)
        {
            var quotesSpan = Layout.Quotes.AsSpan();
            return quotesSpan.IsEmpty ? memberValue : memberValue.Trim(quotesSpan);
        }
    }
}