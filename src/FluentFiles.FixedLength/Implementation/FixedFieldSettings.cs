namespace FluentFiles.FixedLength.Implementation
{
    using System;
    using System.Reflection;
    using FluentFiles.Core.Base;

    internal class FixedFieldSettings : FieldSettingsBase, IFixedFieldSettingsContainer
    {
        public FixedFieldSettings(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }

        public FixedFieldSettings(PropertyInfo propertyInfo, IFixedFieldSettings settings)
            : base(propertyInfo, settings)
        {
            Length = settings.Length;
            PadLeft = settings.PadLeft;
            PaddingChar = settings.PaddingChar;
            SkipWhile = settings.SkipWhile;
            TakeUntil = settings.TakeUntil;
            TruncateIfExceedFieldLength = settings.TruncateIfExceedFieldLength;
            StringNormalizer = settings.StringNormalizer;
        }

        public int Length { get; set; }
        public bool PadLeft { get; set; }
        public char PaddingChar { get; set; }
        public Func<char, int, bool> SkipWhile { get; set; }
        public Func<char, int, bool> TakeUntil { get; set; }
        public bool TruncateIfExceedFieldLength { get; set; }
        public Func<string, string> StringNormalizer { get; set; }
    }
}