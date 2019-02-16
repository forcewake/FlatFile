namespace FluentFiles.FixedLength.Implementation
{
    using System;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    /// <summary>
    /// Converts lines of a fixed-length file into record instances.
    /// </summary>
    public sealed class FixedLengthLineParser : LineParserBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
                                                IFixedLengthLineParser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FixedLengthLineParser"/>.
        /// </summary>
        /// <param name="layout">Describes how a file record maps to a type.</param>
        public FixedLengthLineParser(ILayoutDescriptor<IFixedFieldSettingsContainer> layout)
            : base(layout)
        {
        }

        /// <summary>
        /// Maps a line of a fixed-length file to an instance of <typeparamref name="TRecord" />.
        /// </summary>
        /// <typeparam name="TRecord">The type to map to.</typeparam>
        /// <param name="line">The file line to parse.</param>
        /// <param name="entity">The instance to populate.</param>
        /// <returns>An instance of <typeparamref name="TRecord" /> populated with the parsed and transformed data from <paramref name="line" />.</returns>
        public override TRecord ParseLine<TRecord>(ReadOnlySpan<char> line, TRecord entity)
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

        private static ReadOnlySpan<char> GetValueFromLine(ReadOnlySpan<char> line, int linePosition, IFixedFieldSettingsContainer field)
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

        /// <summary>
        /// Performs fixed-length file specific string pre-processing, such as removing padding from a field when necessary.
        /// </summary>
        /// <param name="field">The field mapping.</param>
        /// <param name="memberValue">The field value.</param>
        /// <returns>A pre-processed field value.</returns>
        protected override ReadOnlySpan<char> PreprocessFieldValue(IFixedFieldSettingsContainer field, ReadOnlySpan<char> memberValue)
        {
            var trimmed = field.PadLeft
                ? memberValue.TrimStart(field.PaddingChar)
                : memberValue.TrimEnd(field.PaddingChar);

            var skipped = Skip(field, trimmed);
            var taken = Take(field, skipped);
            return taken;
        }

        private ReadOnlySpan<char> Skip(IFixedFieldSettingsContainer field, in ReadOnlySpan<char> value)
        {
            if (field.SkipWhile != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    var canContinue = field.SkipWhile(value[i], i);
                    if (!canContinue)
                        return value.Slice(i);
                }
            }

            return value;
        }

        private ReadOnlySpan<char> Take(IFixedFieldSettingsContainer field, in ReadOnlySpan<char> value)
        {
            if (field.TakeUntil != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    var canStop = field.TakeUntil(value[i], i);
                    if (canStop)
                        return value.Slice(0, i);
                }
            }

            return value;
        }
    }
}