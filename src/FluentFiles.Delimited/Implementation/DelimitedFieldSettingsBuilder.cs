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
        private ITypeConverter _typeConverter;

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
            _typeConverter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }

        public IDelimitedFieldSettingsBuilder WithConversionFromString<TProperty>(Func<string, TProperty> conversion)
        {
            if (_typeConverter == null)
                _typeConverter = new DelegatingTypeConverter<TProperty>();

            if (_typeConverter is DelegatingTypeConverter<TProperty>)
                ((DelegatingTypeConverter<TProperty>)_typeConverter).ConversionFromString = conversion;
            else
                throw new InvalidOperationException("A type converter has already been explicitly set.");

            return this;
        }

        public IDelimitedFieldSettingsBuilder WithConversionToString<TProperty>(Func<TProperty, string> conversion)
        {
            if (_typeConverter == null)
                _typeConverter = new DelegatingTypeConverter<TProperty>();

            if (_typeConverter is DelegatingTypeConverter<TProperty>)
                ((DelegatingTypeConverter<TProperty>)_typeConverter).ConversionToString = conversion;
            else
                throw new InvalidOperationException("A type converter has already been explicitly set.");

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
                TypeConverter = _typeConverter
            };
        }
    }
}