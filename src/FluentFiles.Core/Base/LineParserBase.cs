namespace FluentFiles.Core.Base
{
    using System;

    public abstract class LineParserBase<TLayoutDescriptor, TFieldSettings> : ILineParser
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        protected LineParserBase(TLayoutDescriptor layout)
        {
            Layout = layout;
        }

        protected TLayoutDescriptor Layout { get; }

        public abstract TEntity ParseLine<TEntity>(ReadOnlySpan<char> line, TEntity entity) where TEntity : new();

        protected virtual object GetFieldValueFromString(TFieldSettings fieldSettings, ReadOnlySpan<char> memberValue)
        {
            if (fieldSettings.IsNullable && memberValue.Trim('"').Equals(fieldSettings.NullValue.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var preprocessed = PreprocessFieldValue(fieldSettings, memberValue);

            var value = ConvertFromString(fieldSettings, preprocessed);
            return value;
        }

        protected virtual ReadOnlySpan<char> PreprocessFieldValue(TFieldSettings field, ReadOnlySpan<char> memberValue) => memberValue;

        private static object ConvertFromString(TFieldSettings field, ReadOnlySpan<char> source)
        {
            var converter = field.Converter;
            if (converter != null && converter.CanConvert(from: typeof(string), to: field.Type))
                return converter.ConvertFromString(source, field.PropertyInfo);

            return null;
        }
    }
}