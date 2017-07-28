namespace FlatFile.FixedLength.Attributes
{
    using System;
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

        public bool TruncateIfExceedFieldLength { get; set; }

        public Func<string, string> StringNormalizer { get; set; }

        public FixedLengthFieldAttribute(int index, int length, bool truncateIfExceed = false)
            : base(index)
        {
            Padding = Padding.Left;
            TruncateIfExceedFieldLength = truncateIfExceed;
            Length = length;
        }
    }
}