using System;

namespace FluentFiles.FixedLength.Implementation
{
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    public class FixedLengthLineParser :
        LineParserBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineParser
    {
        public FixedLengthLineParser(ILayoutDescriptor<IFixedFieldSettingsContainer> layout)
            : base(layout)
        {
        }

        public override TEntity ParseLine<TEntity>(in ReadOnlySpan<char> line, TEntity entity)
        {
            int linePosition = 0;
            foreach (var field in Layout.Fields)
            {
                var fieldValueFromLine = GetValueFromLine(line, linePosition, field);
                object convertedFieldValue = GetFieldValueFromString(field, fieldValueFromLine);
                field.SetValueOf(entity, convertedFieldValue);
                linePosition += field.Length;
            }
            return entity;
        }

        private static ReadOnlySpan<char> GetValueFromLine(in ReadOnlySpan<char> line, int linePosition, IFixedFieldSettingsContainer field)
        {
            if (linePosition + field.Length > line.Length)
            {
                if ((linePosition + field.Length) - line.Length != field.Length && linePosition <= line.Length)
                {
                    return line.Slice(linePosition);
                }

                if (field.IsNullable)
                {
                    return field.NullValue.AsSpan();
                }

                throw new IndexOutOfRangeException(
                    string.Format("The field at {0} with a length of {1} cannot be found on the line because the line is too short. " +
                    "Setting a NullValue for this field will allow the line to be parsed and this field to be null.", field.Index, field.Length));
            }

            return line.Slice(linePosition, field.Length);
        }

        protected override ReadOnlySpan<char> PreprocessFieldValue(IFixedFieldSettingsContainer field, in ReadOnlySpan<char> memberValue)
        {
            return field.PadLeft
                ? memberValue.TrimStart(field.PaddingChar)
                : memberValue.TrimEnd(field.PaddingChar);
        }
    }
}