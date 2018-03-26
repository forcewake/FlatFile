namespace FlatFile.Core.Base
{
    using System;
    using FlatFile.Core.Extensions;

    public abstract class LineParserBase<TLayoutDescriptor, TFieldSettings> : ILineParser
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        private readonly TLayoutDescriptor _layout;

        protected LineParserBase(TLayoutDescriptor layout)
        {
            this._layout = layout;
        }

        protected TLayoutDescriptor Layout
        {
            get { return _layout; }
        }

        public abstract TEntity ParseLine<TEntity>(string line, TEntity entity) where TEntity : new();

        protected virtual object GetFieldValueFromString(TFieldSettings fieldSettings, string memberValue)
        {
            if (fieldSettings.IsNullable && memberValue.Trim('"').Equals(fieldSettings.NullValue, StringComparison.CurrentCultureIgnoreCase))
            {
                return null;
            }

            var type = fieldSettings.PropertyInfo.PropertyType;

            if (fieldSettings.IsNullablePropertyType())
            {
                type = Nullable.GetUnderlyingType(fieldSettings.PropertyInfo.PropertyType);
            }

            memberValue = TransformStringValue(fieldSettings, memberValue);

            object obj;
            
            if (!fieldSettings.TypeConverter.ConvertFromStringTo(memberValue, type, out obj))
            {
                obj = memberValue.Convert(type);
            }

            return obj;
        }

        protected virtual string TransformStringValue(TFieldSettings fieldSettingsBuilder, string memberValue)
        {
            return memberValue;
        }
    }

    public static class TypeConverterExtensions
    {

        public static bool ConvertFromStringTo(this ITypeConverter converter, string source, Type targetType, out object obj)
        {
            if (converter != null && converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(targetType))
            {
                obj = converter.ConvertFromString(source);
                return true;
            }

            obj = null;
            
            return false;
        }
    }
}