using System;

namespace FluentFiles.Core.Conversion
{
    /// <summary>
    /// Adapts an <see cref="ITypeConverter"/> to an <see cref="IFieldValueConverter"/>.
    /// </summary>
    public class FieldValueConverterAdapter : IFieldValueConverter
    {
        private readonly ITypeConverter _converter;

        /// <summary>
        /// Intializes a new instance of <see cref="FieldValueConverterAdapter"/> with an <see cref="ITypeConverter"/>.
        /// </summary>
        /// <param name="converter">The <see cref="ITypeConverter"/> to wrap.</param>
        public FieldValueConverterAdapter(ITypeConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public bool CanConvert(Type from, Type to) => _converter.CanConvertFrom(from) && _converter.CanConvertTo(to);

        public object ConvertFromString(in FieldDeserializationContext context) => _converter.ConvertFromString(context.Source.ToString());

        public string ConvertToString(in FieldSerializationContext context) => _converter.ConvertToString(context.Source);
    }
}
