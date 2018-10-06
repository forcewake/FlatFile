namespace FluentFiles.Core.Base
{
    public abstract class LineBuilderBase<TLayoutDescriptor, TFieldSettings> : ILineBuilder
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        protected LineBuilderBase(TLayoutDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        protected TLayoutDescriptor Descriptor { get; }

        public abstract string BuildLine<T>(T entry);

        protected virtual string GetStringValueFromField(TFieldSettings field, object fieldValue)
        {
            string lineValue = fieldValue != null
                ? ConvertToString(field, fieldValue)
                : field.NullValue ?? string.Empty;

            lineValue = PostprocessFieldValue(field, lineValue);

            return lineValue;
        }

        protected virtual string PostprocessFieldValue(TFieldSettings field, string value) => value;

        private static string ConvertToString(TFieldSettings field, object fieldValue)
        {
            var converter = field.TypeConverter;
            if (converter != null && converter.CanConvert(from: field.Type, to: typeof(string)))
                return converter.ConvertToString(fieldValue, field.PropertyInfo).ToString();

            return fieldValue.ToString();
        }
    }
}