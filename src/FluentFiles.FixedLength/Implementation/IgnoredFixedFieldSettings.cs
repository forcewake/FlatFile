namespace FluentFiles.FixedLength
{
    using System;
    using System.Reflection;
    using FluentFiles.Core.Conversion;

    internal class IgnoredFixedFieldSettings : IFixedFieldSettingsContainer
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
        public IFieldValueConverter Converter { get; } = null;
        public bool PadLeft { get; } = false;
        public char PaddingChar { get; } = default;

        public Func<char, int, bool> SkipWhile { get; } = null;
        public Func<char, int, bool> TakeUntil { get; } = null;

        public bool TruncateIfExceedFieldLength { get; } = false;
        public Func<string, string> StringNormalizer { get; } = null;
        public Type Type { get; } = typeof(string);
        public PropertyInfo PropertyInfo { get; } = null;

        public object GetValueOf(object instance) => throw new NotSupportedException("Cannot use a fixed width layout with an ignored section for writing.");
        public void SetValueOf(object instance, object value) { /* no-op */ }
    }
}