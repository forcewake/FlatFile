using System;

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
                string fieldValueFromLine = GetValueFromLine(line, linePosition, field);
                object convertedFieldValue = GetFieldValueFromString(field, fieldValueFromLine);
                field.PropertyInfo.SetValue(entity, convertedFieldValue, null);
                linePosition += field.Length;
            }
            return entity;
        }

        private static string GetValueFromLine(string line, int linePosition, IFixedFieldSettingsContainer field)
        {
            if (linePosition + field.Length > line.Length)
            {
                if ((linePosition + field.Length) - line.Length != field.Length)
                {
                    return line.Substring(linePosition);
                }

                if (field.IsNullable)
                {
                    return field.NullValue;
                }

                throw new IndexOutOfRangeException(
                    $"The field at {field.Index} with a length of {field.Length} cannot be found on the line because the line is too short." +
                    "Setting IsNullable to true will allow the line to be parsed");
            }

            return line.Substring(linePosition, field.Length);
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