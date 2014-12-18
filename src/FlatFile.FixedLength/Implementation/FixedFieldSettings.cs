namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core.Base;

    public class FixedFieldSettings : FieldSettingsBase
    {
        public int Lenght { get; set; }
        public bool PadLeft { get; set; }
        public char PaddingChar { get; set; }
    }
}