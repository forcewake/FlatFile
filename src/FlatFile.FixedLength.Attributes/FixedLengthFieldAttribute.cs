namespace FlatFile.FixedLength.Attributes
{
    using FlatFile.Core.Attributes.Base;

    public class FixedLengthFieldAttribute : FieldSettingsBaseAttribute, IFixedFieldSettings
    {
        public int Length { get; protected set; }
        
        public Padding Padding { get; set; }

        public bool PadLeft
        {
            get { return Padding == Padding.Left; }
        }

        public char PaddingChar { get; set; }

        public FixedLengthFieldAttribute(int index, int length)
            : base(index)
        {
            Padding = Padding.Left;

            Length = length;
        }
    }
}