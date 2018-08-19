using System;

namespace FluentFiles.FixedLength
{
    using System.Reflection;
    using FluentFiles.Core.Base;

    public interface IFixedFieldSettings : IFieldSettings
    {
        int Length { get; }
        bool PadLeft { get; }
        char PaddingChar { get; }
        bool TruncateIfExceedFieldLength { get; }
        Func<string, string> StringNormalizer { get; }
    }

    public interface IFixedFieldSettingsContainer : IFixedFieldSettings, IFieldSettingsContainer
    {
    }

    public class FixedFieldSettings : FieldSettingsBase, IFixedFieldSettingsContainer
    {
        public FixedFieldSettings(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public FixedFieldSettings(IFixedFieldSettings settings)
            : base(settings)
        {
            Length = settings.Length;
            PadLeft = settings.PadLeft;
            PaddingChar = settings.PaddingChar;
            TypeConverter = settings.TypeConverter;
            TruncateIfExceedFieldLength = settings.TruncateIfExceedFieldLength;
            StringNormalizer = settings.StringNormalizer;
        }

        public FixedFieldSettings(PropertyInfo propertyInfo, IFixedFieldSettings settings)
            : this(settings)
        {
            PropertyInfo = propertyInfo;
        }

        public int Length { get; set; }
        public bool PadLeft { get; set; }
        public char PaddingChar { get; set; }
        public bool TruncateIfExceedFieldLength { get; set; }
        public Func<string, string> StringNormalizer { get; set; }
    }
}