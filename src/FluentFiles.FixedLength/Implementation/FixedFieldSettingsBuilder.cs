using System;

namespace FluentFiles.FixedLength.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;
    using FluentFiles.Core.Extensions;

    public class FixedFieldSettingsBuilder : IFixedFieldSettingsBuilder
    {
        private readonly PropertyInfo _property;
        private bool _isNullable;
        private string _nullValue;
        private bool _truncateIfExceedFieldLength;
        private Func<string, string> _stringNormalizer;
        private int _length;
        private char _paddingChar;
        private bool _padLeft;
        private ITypeConverter _typeConverter;

        public FixedFieldSettingsBuilder(PropertyInfo property)
        {
            _property = property;
        }

        public IFixedFieldSettingsBuilder TruncateFieldContentIfExceedLength()
        {
            _truncateIfExceedFieldLength = true;
            return this;
        }

        public IFixedFieldSettingsBuilder WithStringNormalizer(Func<string, string> stringNormalizer)
        {
            _stringNormalizer = stringNormalizer;
            return this;
        }

        public IFixedFieldSettingsBuilder WithLength(int length)
        {
            _length = length;
            return this;
        }

        public IFixedFieldSettingsBuilder WithLeftPadding(char paddingChar)
        {
            _paddingChar = paddingChar;
            _padLeft = true;
            return this;
        }

        public IFixedFieldSettingsBuilder WithRightPadding(char paddingChar)
        {
            _paddingChar = paddingChar;
            _padLeft = false;
            return this;
        }

        public IFixedFieldSettingsBuilder AllowNull(string nullValue)
        {
            _isNullable = true;
            _nullValue = nullValue;
            return this;
        }

        public IFixedFieldSettingsBuilder WithTypeConverter<TConverter>() where TConverter : ITypeConverter, new()
        {
            return WithTypeConverter(ReflectionHelper.CreateInstance<TConverter>(true));
        }

        public IFixedFieldSettingsBuilder WithTypeConverter(ITypeConverter converter)
        {
            _typeConverter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }

        public IFixedFieldSettingsBuilder WithConversionFromString<TProperty>(Func<string, TProperty> conversion)
        {
            if (_typeConverter == null)
                _typeConverter = new DelegatingTypeConverter<TProperty>();

            if (_typeConverter is DelegatingTypeConverter<TProperty>)
                ((DelegatingTypeConverter<TProperty>)_typeConverter).ConversionFromString = conversion;
            else
                throw new InvalidOperationException("A type converter has already been explicitly set.");

            return this;
        }

        public IFixedFieldSettingsBuilder WithConversionToString<TProperty>(Func<TProperty, string> conversion)
        {
            if (_typeConverter == null)
                _typeConverter = new DelegatingTypeConverter<TProperty>();

            if (_typeConverter is DelegatingTypeConverter<TProperty>)
                ((DelegatingTypeConverter<TProperty>)_typeConverter).ConversionToString = conversion;
            else
                throw new InvalidOperationException("A type converter has already been explicitly set.");

            return this;
        }

        public IFixedFieldSettingsContainer Build()
        {
            return new FixedFieldSettings(_property)
            {
                IsNullable = _isNullable,
                NullValue = _nullValue,
                PaddingChar = _paddingChar,
                PadLeft = _padLeft,
                Length = _length,
                StringNormalizer = _stringNormalizer,
                TruncateIfExceedFieldLength = _truncateIfExceedFieldLength,
                TypeConverter = _typeConverter
            };
        }
    }
}