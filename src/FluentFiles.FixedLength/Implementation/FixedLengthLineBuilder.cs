namespace FluentFiles.FixedLength.Implementation
{
    using System;
    using System.Linq;
    using System.Text;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    public class FixedLengthLineBuilder : LineBuilderBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineBuilder
    {
        private readonly Lazy<int> _totalLength;

        public FixedLengthLineBuilder(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
            : base(descriptor, TransformFieldValue)
        {
            _totalLength = new Lazy<int>(() => descriptor.Fields.Sum(f => f.Length));
        }

        public override string BuildLine<T>(T entry)
        {
            var line = new StringBuilder(_totalLength.Value);
            foreach (var field in Descriptor.Fields)
                line.Append(GetStringValueFromField(field, field.GetValueOf(entry)));

            return line.ToString();
        }

        private static string TransformFieldValue(IFixedFieldSettingsContainer field, string lineValue)
        {
            if (field.StringNormalizer != null)
            {
                lineValue = field.StringNormalizer(lineValue);
            }

            if (lineValue.Length >= field.Length)
            {
                return field.TruncateIfExceedFieldLength ? lineValue.Substring(0, field.Length) : lineValue;
            }

            lineValue = field.PadLeft
                ? lineValue.PadLeft(field.Length, field.PaddingChar)
                : lineValue.PadRight(field.Length, field.PaddingChar);

            return lineValue;
        }
    }
}