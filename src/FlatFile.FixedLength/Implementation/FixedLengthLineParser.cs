namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core.Base;

    public class FixedLengthLineParser<T> :
        LineParserBase<T, IFixedLayout<T>, FixedFieldSettings, IFixedFieldSettingsConstructor>,
        IFixedLengthLineParser<T>
        where T : new()
    {
        public FixedLengthLineParser(IFixedLayout<T> layout) : base(layout)
        {
        }

        public override T ParseLine(string line, T entry)
        {
            int linePosition = 0;
            foreach (FixedFieldSettings field in Layout.Fields)
            {
                string fieldValueFromLine = line.Substring(linePosition, field.Lenght);
                object convertedFieldValue = GetFieldValueFromString(field, fieldValueFromLine);
                field.PropertyInfo.SetValue(entry, convertedFieldValue, null);
                linePosition += field.Lenght;
            }
            return entry;
        }

        protected override string TransformStringValue(FixedFieldSettings fieldSettingsBuilder, string memberValue)
        {
            memberValue = fieldSettingsBuilder.PadLeft
                ? memberValue.TrimStart(new[] {fieldSettingsBuilder.PaddingChar})
                : memberValue.TrimEnd(new[] {fieldSettingsBuilder.PaddingChar});

            return memberValue;
        }
    }
}