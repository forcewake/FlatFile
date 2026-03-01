namespace FlatFile.FixedLength.Implementation
{
    using System.Text;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLengthLineBuilder :
        LineBulderBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineBuilder
    {
        public FixedLengthLineBuilder(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
            : base(descriptor)
        {
        }

        public override string BuildLine<T>(T entry)
        {
            var lineBuilder = new StringBuilder();

            foreach (var field in Descriptor.Fields)
            {
                lineBuilder.Append(GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null)));
            }

            return lineBuilder.ToString();
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
