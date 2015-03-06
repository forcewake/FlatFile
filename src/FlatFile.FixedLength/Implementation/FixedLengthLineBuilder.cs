namespace FlatFile.FixedLength.Implementation
{
    using System.Linq;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLengthLineBuilder<T> :
        LineBulderBase<T, ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineBuilder<T>
    {
        public FixedLengthLineBuilder(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
            : base(descriptor)
        {
        }

        public override string BuildLine(T entry)
        {
            string line = Descriptor.Fields.Aggregate(string.Empty,
                (current, field) => current + GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null)));
            return line;
        }

        protected override string TransformFieldValue(IFixedFieldSettingsContainer field, string lineValue)
        {
            if (lineValue.Length >= field.Length)
            {
                return lineValue;
            }

            lineValue = field.PadLeft
                ? lineValue.PadLeft(field.Length, field.PaddingChar)
                : lineValue.PadRight(field.Length, field.PaddingChar);

            return lineValue;
        }
    }
}