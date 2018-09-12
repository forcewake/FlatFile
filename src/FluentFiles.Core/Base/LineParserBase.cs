namespace FluentFiles.Core.Base
{
    using System;
    using System.Reflection;
    using FluentFiles.Core.Extensions;

    public abstract class LineParserBase<TLayoutDescriptor, TFieldSettings> : ILineParser
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        protected LineParserBase(TLayoutDescriptor layout)
        {
            this.Layout = layout;
        }

        protected TLayoutDescriptor Layout { get; }

        public abstract TEntity ParseLine<TEntity>(in ReadOnlySpan<char> line, TEntity entity) where TEntity : new();

        protected virtual object GetFieldValueFromString(TFieldSettings fieldSettings, in ReadOnlySpan<char> memberValue)
        {
            if (fieldSettings.IsNullable && memberValue.Trim('"').Equals(fieldSettings.NullValue.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var preprocessed = PreprocessFieldValue(fieldSettings, memberValue);

            var value = ConvertFromStringTo(fieldSettings.TypeConverter, preprocessed, fieldSettings.Type.Unwrap(), fieldSettings.PropertyInfo);
            return value;
        }

        protected virtual ReadOnlySpan<char> PreprocessFieldValue(TFieldSettings fieldSettingsBuilder, in ReadOnlySpan<char> memberValue) => memberValue;

        private static object ConvertFromStringTo(ITypeConverter converter, in ReadOnlySpan<char> source, Type targetType, PropertyInfo targetProperty)
        {
            if (converter != null && converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(targetType))
            {
                return converter.ConvertFromString(source.ToString(), targetProperty);
            }

            return null;
        }
    }
}