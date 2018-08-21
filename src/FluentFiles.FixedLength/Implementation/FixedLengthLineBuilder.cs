namespace FluentFiles.FixedLength.Implementation
{
    using System.Linq;
    using System.Text;
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
            var builder = new StringBuilder(Descriptor.Fields.Sum(f => f.Length));
            var line = Descriptor.Fields.Aggregate(builder,
                (current, field) => current.Append(GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null))));
            return line.ToString();
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