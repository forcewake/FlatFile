namespace FlatFile.FixedLength.Implementation
{
    using System.Reflection;

    public class FixedFieldSettingsConstructor : FixedFieldSettings,
        IFixedFieldSettingsConstructor
    {
        public FixedFieldSettingsConstructor(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public IFixedFieldSettingsConstructor WithLenght(int lenght)
        {
            Lenght = lenght;
            return this;
        }

        public IFixedFieldSettingsConstructor WithLeftPadding(char paddingChar)
        {
            PaddingChar = paddingChar;
            PadLeft = true;
            return this;
        }

        public IFixedFieldSettingsConstructor WithRightPadding(char paddingChar)
        {
            PaddingChar = paddingChar;
            PadLeft = false;
            return this;
        }

        public IFixedFieldSettingsConstructor AllowNull(string nullValue)
        {
            IsNullable = true;
            NullValue = nullValue;
            return this;
        }
    }
}