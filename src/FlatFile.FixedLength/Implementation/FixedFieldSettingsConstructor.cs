namespace FlatFile.FixedLength.Implementation
{
    using System.Reflection;
    using FlatFile.Core.Base;

    public class FixedFieldSettingsConstructor :
        FieldSettingsConstructorBase<FixedFieldSettings, IFixedFieldSettingsConstructor>,
        IFixedFieldSettingsConstructor
    {
        public FixedFieldSettingsConstructor(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public int Lenght { get; protected set; }
        public char PaddingChar { get; protected set; }
        public bool PadLeft { get; protected set; }

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

        public override IFixedFieldSettingsConstructor AllowNull(string nullValue)
        {
            IsNullable = true;
            NullValue = nullValue;
            return this;
        }
    }
}