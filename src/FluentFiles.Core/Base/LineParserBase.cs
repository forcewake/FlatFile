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

        public abstract TEntity ParseLine<TEntity>(string line, TEntity entity) where TEntity : new();

        protected virtual object GetFieldValueFromString(TFieldSettings fieldSettings, string memberValue)
        {
            if (fieldSettings.IsNullable && memberValue.Trim('"').Equals(fieldSettings.NullValue, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            var targetType = fieldSettings.PropertyInfo.PropertyType.Unwrap();

            memberValue = PreprocessFieldValue(fieldSettings, memberValue);

            var value = ConvertFromStringTo(fieldSettings.TypeConverter, memberValue, targetType, fieldSettings.PropertyInfo);
            return value;
        }

        protected virtual string PreprocessFieldValue(TFieldSettings fieldSettingsBuilder, string memberValue) => memberValue;

        private static object ConvertFromStringTo(ITypeConverter converter, string source, Type targetType, PropertyInfo targetProperty)
        {
            if (converter != null && converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(targetType))
            {
                return converter.ConvertFromString(source, targetProperty);
            }

            return null;
        }
    }
}