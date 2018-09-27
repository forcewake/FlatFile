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
            : base(descriptor)
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

        protected override string PostprocessFieldValue(IFixedFieldSettingsContainer field, string value)
        {
            if (field.StringNormalizer != null)
            {
                value = field.StringNormalizer(value);
            }

            if (value.Length >= field.Length)
            {
                return field.TruncateIfExceedFieldLength ? value.Substring(0, field.Length) : value;
            }

            value = field.PadLeft
                ? value.PadLeft(field.Length, field.PaddingChar)
                : value.PadRight(field.Length, field.PaddingChar);

            return value;
        }
    }
}