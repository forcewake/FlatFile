namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.Reflection;
    using FluentFiles.Core;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;

    public class DelimitedFieldSettingsBuilder : IDelimitedFieldSettingsBuilder
    {
        private readonly PropertyInfo _property;
        private bool _isNullable;
        private string _nullValue;
        private string _name;
        private IFieldValueConverter _converter;

        public DelimitedFieldSettingsBuilder(PropertyInfo property)
        {
            _property = property;
        }

        public IDelimitedFieldSettingsBuilder AllowNull(string nullValue)
        {
            _isNullable = true;
            _nullValue = nullValue;
            return this;
        }

        public IDelimitedFieldSettingsBuilder WithTypeConverter<TConverter>() where TConverter : ITypeConverter, new()
        {
            return WithTypeConverter(ReflectionHelper.CreateInstance<TConverter>(true));
        }

        public IDelimitedFieldSettingsBuilder WithTypeConverter(ITypeConverter converter)
        {
            var typeConverter = converter ?? throw new ArgumentNullException(nameof(converter));
            return WithConverter(new FieldValueConverterAdapter(typeConverter));
        }

        public IDelimitedFieldSettingsBuilder WithConverter<TConverter>() where TConverter : IFieldValueConverter, new()
        {
            return WithConverter(ReflectionHelper.CreateInstance<TConverter>(true));
        }

        public IDelimitedFieldSettingsBuilder WithConverter(IFieldValueConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }

        public IDelimitedFieldSettingsBuilder WithConversionFromString<TProperty>(ConvertFromString<TProperty> conversion)
        {
            if (_converter == null)
                _converter = new DelegatingConverter<TProperty>();

            if (_converter is DelegatingConverter<TProperty> delegatingConverter)
                delegatingConverter.ConversionFromString = conversion;
            else
                throw new InvalidOperationException("A converter has already been explicitly set.");

            return this;
        }

        public IDelimitedFieldSettingsBuilder WithConversionToString<TProperty>(ConvertToString<TProperty> conversion)
        {
            if (_converter == null)
                _converter = new DelegatingConverter<TProperty>();

            if (_converter is DelegatingConverter<TProperty> delegatingConverter)
                delegatingConverter.ConversionToString = conversion;
            else
                throw new InvalidOperationException("A converter has already been explicitly set.");

            return this;
        }

        public IDelimitedFieldSettingsBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public IDelimitedFieldSettingsContainer Build()
        {
            return new DelimitedFieldSettings(_property)
            {
                IsNullable = _isNullable,
                NullValue = _nullValue,
                Converter = _converter
            };
        }
    }
}