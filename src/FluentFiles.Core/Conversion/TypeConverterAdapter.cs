﻿using System;
using System.ComponentModel;
using System.Reflection;

namespace FluentFiles.Core.Conversion
{
    /// <summary>
    /// Adapts a <see cref="TypeConverter"/> to an <see cref="IValueConverter"/>.
    /// </summary>
    public class TypeConverterAdapter : IValueConverter
    {
        private readonly TypeConverter _converter;

        /// <summary>
        /// Intializes a new instance of <see cref="TypeConverterAdapter"/> with a <see cref="TypeConverter"/>.
        /// </summary>
        /// <param name="converter">The <see cref="TypeConverter"/> to wrap.</param>
        public TypeConverterAdapter(TypeConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public bool CanConvertFrom(Type type) => _converter.CanConvertFrom(type);

        public bool CanConvertTo(Type type) => OverrideCanConvertTo || _converter.CanConvertTo(type);

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => _converter.ConvertFromString(source.ToString());

        public ReadOnlySpan<char> ConvertToString(object source, PropertyInfo sourceProperty) => _converter.ConvertToString(source).AsSpan();

        /// <summary>
        /// Some built-in <see cref="TypeConverter"/>s don't return as expected for <see cref="TypeConverter.CanConvertTo(Type)"/>.
        /// Setting this to true makes sure that call succeeds.
        /// </summary>
        public bool OverrideCanConvertTo { get; set; }
    }
}
