namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLengthLineParser<T> :
        LineParserBase<T, ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineParser<T>
        where T : new()
    {
        public FixedLengthLineParser(ILayoutDescriptor<IFixedFieldSettingsContainer> layout)
            : base(layout)
        {
        }

        public override T ParseLine(string line, T entry)
        {
            int linePosition = 0;
            foreach (var field in Layout.Fields)
            {
                string fieldValueFromLine = line.Substring(linePosition, field.Length);
                object convertedFieldValue = GetFieldValueFromString(field, fieldValueFromLine);
                field.PropertyInfo.SetValue(entry, convertedFieldValue, null);
                linePosition += field.Length;
            }
            return entry;
        }

        protected override string TransformStringValue(IFixedFieldSettingsContainer fieldSettingsBuilder, string memberValue)
        {
            memberValue = fieldSettingsBuilder.PadLeft
                ? memberValue.TrimStart(new[] {fieldSettingsBuilder.PaddingChar})
                : memberValue.TrimEnd(new[] {fieldSettingsBuilder.PaddingChar});

            return memberValue;
        }
    }
}