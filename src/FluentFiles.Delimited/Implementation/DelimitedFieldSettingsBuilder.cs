namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.Reflection;
    using FluentFiles.Core;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;

    /// <summary>
    /// Configures a delimited file field.
    /// </summary>
    public class DelimitedFieldSettingsBuilder : IDelimitedFieldSettingsBuilder
    {
        private readonly MemberInfo _member;
        private bool _isNullable;
        private string _nullValue;
        private string _name;
        private IFieldValueConverter _converter;

        /// <summary>
        /// Initializes a new <see cref="DelimitedFieldSettingsBuilder"/>,
        /// </summary>
        /// <param name="member">The member a field maps to.</param>
        public DelimitedFieldSettingsBuilder(MemberInfo member)
        {
            _member = member ?? throw new ArgumentNullException(nameof(member));
        }

        /// <summary>
        /// Specifies that a field can be null and provides the string value that indicates null data.
        /// </summary>
        /// <param name="nullValue">The string that indicates a null valued field.</param>
        public IDelimitedFieldSettingsBuilder AllowNull(string nullValue)
        {
            _isNullable = true;
            _nullValue = nullValue;
            return this;
        }

        /// <summary>
        /// Specifies that a field's value should be converted using a new instance of the type <typeparamref name="TConverter"/>.
        /// </summary>
        /// <typeparam name="TConverter">The type of <see cref="IFieldValueConverter"/> to use for conversion.</typeparam>
        public IDelimitedFieldSettingsBuilder WithConverter<TConverter>() where TConverter : IFieldValueConverter, new()
        {
            return WithConverter(ReflectionHelper.CreateInstance<TConverter>(true));
        }

        /// <summary>
        ///  Specifies that a field's value should be converted using the provided <see cref="IFieldValueConverter"/> implementation.
        /// </summary>
        /// <param name="converter">The converter to use.</param>
        public IDelimitedFieldSettingsBuilder WithConverter(IFieldValueConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }

        /// <summary>
        /// Specifies that a field's value should be converted from a string to its destination type using the provided conversion function.
        /// </summary>
        /// <typeparam name="TProperty">The type of the destination property.</typeparam>
        /// <param name="parser">A lambda function converting from a string.</param>
        public IDelimitedFieldSettingsBuilder WithConversionFromString<TProperty>(FieldParser<TProperty> parser)
        {
            if (_converter == null)
                _converter = new DelegatingConverter<TProperty>();

            if (_converter is DelegatingConverter<TProperty> delegatingConverter)
                delegatingConverter.ParseValue = parser;
            else
                throw new InvalidOperationException("A converter has already been explicitly set.");

            return this;
        }

        /// <summary>
        /// Specifies that a field's value should be converted to a string from its source type using the provided conversion function.
        /// </summary>
        /// <typeparam name="TProperty">The type of the source property.</typeparam>
        /// <param name="formatter">A lambda function converting to a string.</param>
        public IDelimitedFieldSettingsBuilder WithConversionToString<TProperty>(FieldFormatter<TProperty> formatter)
        {
            if (_converter == null)
                _converter = new DelegatingConverter<TProperty>();

            if (_converter is DelegatingConverter<TProperty> delegatingConverter)
                delegatingConverter.FormatValue = formatter;
            else
                throw new InvalidOperationException("A converter has already been explicitly set.");

            return this;
        }

        /// <summary>
        /// Sets the name to use when writing a field's header.
        /// </summary>
        /// <param name="name">The field's header name.</param>
        public IDelimitedFieldSettingsBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Creates the field configuration.
        /// </summary>
        public IDelimitedFieldSettingsContainer Build()
        {
            return new DelimitedFieldSettings(_member)
            {
                IsNullable = _isNullable,
                NullValue = _nullValue,
                Converter = _converter
            };
        }
    }
}