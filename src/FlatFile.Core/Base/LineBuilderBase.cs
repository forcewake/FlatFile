namespace FlatFile.Core.Base
{
    public abstract class LineBuilderBase<TLayoutDescriptor, TFieldSettings> : ILineBuilder
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer 
    {
        private readonly TLayoutDescriptor _descriptor;

        protected LineBuilderBase(TLayoutDescriptor descriptor)
        {
            this._descriptor = descriptor;
        }

        protected TLayoutDescriptor Descriptor
        {
            get { return _descriptor; }
        }

        public abstract string BuildLine<T>(T entry);

        protected virtual string GetStringValueFromField(TFieldSettings field, object fieldValue)
        {
            string lineValue = fieldValue != null
                ? ConvertToString(field, fieldValue)
                : field.NullValue ?? string.Empty;

            lineValue = TransformFieldValue(field, lineValue);

            return lineValue;
        }

        protected virtual string TransformFieldValue(TFieldSettings field, string lineValue)
        {
            return lineValue;
        }

        private static string ConvertToString(TFieldSettings field, object fieldValue)
        {
            var converter = field.TypeConverter;
            if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(field.PropertyInfo.PropertyType))
                return field.TypeConverter.ConvertToString(fieldValue, field.PropertyInfo);

            return fieldValue.ToString();
        }
    }
}