namespace FlatFile.FixedLength.Implementation
{
    using System.Linq;
    using FlatFile.Core.Base;

    public class FixedLengthLineBuilder<T> :
        LineBulderBase<T, IFixedLayout<T>, FixedFieldSettings>,
        IFixedLengthLineBuilder<T>
    {
        public FixedLengthLineBuilder(IFixedLayout<T> layout)
            : base(layout)
        {
        }

        public override string BuildLine(T entry)
        {
            string line = Layout.Fields.Aggregate(string.Empty,
                (current, field) => current + GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null)));
            return line;
        }

        protected override string TransformFieldValue(FixedFieldSettings field, string lineValue)
        {
            if (lineValue.Length >= field.Lenght)
            {
                return lineValue;
            }

            lineValue = field.PadLeft
                ? lineValue.PadLeft(field.Lenght, field.PaddingChar)
                : lineValue.PadRight(field.Lenght, field.PaddingChar);

            return lineValue;
        }
    }
}