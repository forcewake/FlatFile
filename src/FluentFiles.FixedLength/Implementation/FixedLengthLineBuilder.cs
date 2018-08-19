namespace FluentFiles.FixedLength.Implementation
{
    using System.Linq;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    public class FixedLengthLineBuilder :
        LineBuilderBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineBuilder
    {
        public FixedLengthLineBuilder(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
            : base(descriptor)
        {
        }

        public override string BuildLine<T>(T entry)
        {
            string line = Descriptor.Fields.Aggregate(string.Empty,
                (current, field) => current + GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null)));
            return line;
        }

        protected override string TransformFieldValue(IFixedFieldSettingsContainer field, string lineValue)
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