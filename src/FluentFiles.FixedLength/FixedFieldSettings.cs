using System;

namespace FluentFiles.FixedLength
{
    using System.Reflection;
    using FluentFiles.Core.Base;
    using FluentFiles.Core.Conversion;

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
            TruncateIfExceedFieldLength = settings.TruncateIfExceedFieldLength;
            StringNormalizer = settings.StringNormalizer;
        }

        public int Length { get; set; }
        public bool PadLeft { get; set; }
        public char PaddingChar { get; set; }
        public bool TruncateIfExceedFieldLength { get; set; }
        public Func<string, string> StringNormalizer { get; set; }
    }

    public class IgnoredFixedFieldSettings : IFixedFieldSettingsContainer
    {
        public IgnoredFixedFieldSettings(int length)
        {
            Length = length;
            UniqueKey = Guid.NewGuid().ToString();
        }
        
        public int? Index { get; set; }

        public int Length { get; }
        public string UniqueKey { get; }
        public bool IsNullable { get; } = false;
        public string NullValue { get; } = null;
        public IValueConverter TypeConverter { get; } = null;
        public bool PadLeft { get; } = false;
        public char PaddingChar { get; } = default;
        public bool TruncateIfExceedFieldLength { get; } = false;
        public Func<string, string> StringNormalizer { get; } = null;
        public Type Type { get; } = typeof(string);
        public PropertyInfo PropertyInfo { get; } = null;

        public object GetValueOf(object instance) => throw new NotSupportedException("Cannot use a fixed width layout with an ignored section for writing.");
        public void SetValueOf(object instance, object value) { /* no-op */ }
    }
}