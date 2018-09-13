using System;

namespace FluentFiles.Core.Base
{
    public abstract class LineBuilderBase<TLayoutDescriptor, TFieldSettings> : ILineBuilder
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer 
    {
        private readonly Func<TFieldSettings, string, string> _valueTransformer;

        protected LineBuilderBase(TLayoutDescriptor descriptor, Func<TFieldSettings, string, string> valueTransformer = null)
        {
            Descriptor = descriptor;
            _valueTransformer = valueTransformer;
        }

        protected TLayoutDescriptor Descriptor { get; }

        public abstract string BuildLine<T>(T entry);

        protected virtual string GetStringValueFromField(TFieldSettings field, object fieldValue)
        {
            string lineValue = fieldValue != null
                ? ConvertToString(field, fieldValue)
                : field.NullValue ?? string.Empty;

            lineValue = _valueTransformer?.Invoke(field, lineValue) ?? lineValue;

            return lineValue;
        }

        private static string ConvertToString(TFieldSettings field, object fieldValue)
        {
            var converter = field.TypeConverter;
            if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(field.Type))
                return field.TypeConverter.ConvertToString(fieldValue, field.PropertyInfo).ToString();

            return fieldValue.ToString();
        }
    }
}