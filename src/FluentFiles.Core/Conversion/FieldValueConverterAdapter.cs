using System;
using System.Reflection;

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

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => _converter.ConvertFromString(source.ToString());

        public string ConvertToString(object source, PropertyInfo sourceProperty) => _converter.ConvertToString(source);
    }
}
