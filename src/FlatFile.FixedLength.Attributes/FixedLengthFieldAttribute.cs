namespace FlatFile.FixedLength.Attributes
{
    using System.Reflection;
    using FlatFile.Core.Attributes.Base;

    public class FixedLengthFieldAttribute : FieldSettingsBaseAttribute
    {
        public int Lenght { get; protected set; }
        
        public Padding Padding { get; set; }

        public bool PadLeft
        {
            get { return Padding == Padding.Left; }
        }

        public char PaddingChar { get; set; }

        public FixedLengthFieldAttribute(int index, int lenght)
            : base(index)
        {
            Padding = Padding.Left;

            Lenght = lenght;
        }

        public FixedFieldSettings GetFieldSettings(PropertyInfo propertyInfo)
        {
            return new FixedFieldSettings
            {
                Index = this.Index,
                IsNullable = this.IsNullable,
                Lenght = this.Lenght,
                NullValue = this.NullValue,
                PaddingChar = this.PaddingChar,
                PadLeft = this.PadLeft,
                PropertyInfo = propertyInfo
            };
        }
    }
}