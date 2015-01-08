namespace FlatFile.Delimited.Implementation
{
    using System;
    using FlatFile.Core.Base;

    public class DelimitedLineParser<TEntry> :
        LineParserBase<TEntry, IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>,
        IDelimitedLineParser<TEntry> 
        where TEntry : new()
    {
        public DelimitedLineParser(IDelimitedLayoutDescriptor layout)
            : base(layout)
        {
        }

        public override TEntry ParseLine(string line, TEntry entry)
        {
            int linePosition = 0;
            int delimiterSize = Layout.Delimiter.Length;
            foreach (var field in Layout.Fields)
            {
                int nextDelimiterIndex = -1;
                if (line.Length > linePosition + delimiterSize)
                {
                    nextDelimiterIndex = line.IndexOf(Layout.Delimiter, linePosition, StringComparison.InvariantCultureIgnoreCase);
                }
                int fieldLenght;
                if (nextDelimiterIndex > -1)
                {
                    fieldLenght = nextDelimiterIndex - linePosition;
                }
                else
                {
                    fieldLenght = line.Length - linePosition;
                }
                string fieldValueFromLine = line.Substring(linePosition, fieldLenght);
                var convertedFieldValue = GetFieldValueFromString(field, fieldValueFromLine);
                field.PropertyInfo.SetValue(entry, convertedFieldValue, null);
                linePosition += fieldLenght + (nextDelimiterIndex > -1 ? delimiterSize : 0);
            }
            return entry;
        }

        protected override string TransformStringValue(IDelimitedFieldSettingsContainer fieldSettingsBuilder, string memberValue)
        {
            if (string.IsNullOrEmpty(Layout.Quotes))
            {
                return memberValue;
            }

            var value = memberValue.Replace(Layout.Quotes, String.Empty);
            return value;
        }
    }
}