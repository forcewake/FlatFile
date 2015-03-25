namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLengthLineParser :
        LineParserBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineParser
    {
        public FixedLengthLineParser(ILayoutDescriptor<IFixedFieldSettingsContainer> layout)
            : base(layout)
        {
        }

        public override TEntity ParseLine<TEntity>(string line, TEntity entity)
        {
            int linePosition = 0;
            foreach (var field in Layout.Fields)
            {
                string fieldValueFromLine = line.Substring(linePosition, field.Length);
                object convertedFieldValue = GetFieldValueFromString(field, fieldValueFromLine);
                field.PropertyInfo.SetValue(entity, convertedFieldValue, null);
                linePosition += field.Length;
            }
            return entity;
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