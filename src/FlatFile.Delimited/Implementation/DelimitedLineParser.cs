namespace FlatFile.Delimited.Implementation
{
    using System;
    using FlatFile.Core;
    using FlatFile.Core.Base;
    using FlatFile.Core.Extensions;

    public class DelimitedLineParser :
        LineParserBase<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>,
        IDelimitedLineParser

    {
        public DelimitedLineParser(IDelimitedLayoutDescriptor layout)
            : base(layout)
        {
        }


        public override TEntity ParseLine<TEntity>(string line, TEntity entity)
        {

            int linePosition = 0;
            int delimiterSize = Layout.Delimiter.Length;
            foreach (var field in Layout.Fields)
            {
                int nextDelimiterIndex = -1;
                if (line.Length > linePosition + delimiterSize)
                {
                    if (!String.IsNullOrEmpty(Layout.Quotes))
                    {
                        if (line.AsSpan(linePosition).StartsWith(Layout.Quotes.AsSpan(), StringComparison.Ordinal))
                        {
                            nextDelimiterIndex = line.IndexOf(Layout.Quotes, linePosition + 1, StringComparison.Ordinal);
                            if (nextDelimiterIndex > -1 && line.Length > nextDelimiterIndex)
                            {
                                nextDelimiterIndex = line.IndexOf(Layout.Delimiter, nextDelimiterIndex, StringComparison.Ordinal);
                            }
                        }
                    }

                    if (nextDelimiterIndex == -1)
                    {
                        nextDelimiterIndex = line.IndexOf(Layout.Delimiter, linePosition, StringComparison.Ordinal);
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
                string fieldValueFromLine = line.Substring(linePosition, fieldLength);
                var convertedFieldValue = GetFieldValueFromString(field, fieldValueFromLine);
                PropertyAccessorCache.SetValue(field.PropertyInfo, entity, convertedFieldValue);
                linePosition += fieldLength + (nextDelimiterIndex > -1 ? delimiterSize : 0);
            }
            return entity;
        }

        protected override string TransformStringValue(IDelimitedFieldSettingsContainer fieldSettingsBuilder, string memberValue)
        {
            if (string.IsNullOrEmpty(Layout.Quotes))
            {
                return memberValue;
            }

            if (memberValue.IndexOf(Layout.Quotes, StringComparison.Ordinal) < 0)
            {
                return memberValue;
            }

            return memberValue.Replace(Layout.Quotes, string.Empty);
        }
    }
}