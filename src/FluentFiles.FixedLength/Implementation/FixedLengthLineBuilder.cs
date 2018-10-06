namespace FluentFiles.FixedLength.Implementation
{
    using System.Linq;
    using System.Text;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;
    using FluentFiles.Core.Extensions;
    using Microsoft.Extensions.ObjectPool;

    public class FixedLengthLineBuilder : LineBuilderBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineBuilder
    {
        private readonly StringBuilder _lineBuilder;

        public FixedLengthLineBuilder(IFixedLengthLayoutDescriptor descriptor)
            : base(descriptor)
        {
            _lineBuilder = new StringBuilder(descriptor.Fields.Sum(f => f.Length));
        }

        public override string BuildLine<T>(T entry)
        {
            _lineBuilder.Clear();
            foreach (var field in Descriptor.Fields)
                _lineBuilder.Append(GetStringValueFromField(field, field.GetValueOf(entry)));

            return _lineBuilder.ToString();
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