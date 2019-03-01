using System;

namespace FluentFiles.FixedLength.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;

    /// <summary>
    /// Configures a fixed-length file field.
    /// </summary>
    public class FixedFieldSettingsBuilder : IFixedFieldSettingsBuilder
    {
        private readonly MemberInfo _member;
        private bool _isNullable;
        private string _nullValue;
        private bool _truncateIfExceedFieldLength;
        private Func<string, string> _stringNormalizer;
        private int _length;
        private char _paddingChar;
        private bool _padLeft;
        private Func<char, int, bool> _skipWhile;
        private Func<char, int, bool> _takeUntil;
        private IFieldValueConverter _converter;

        /// <summary>
        /// Initializes a new <see cref="FixedFieldSettingsBuilder"/>,
        /// </summary>
        /// <param name="member">The member a field maps to.</param>
        public FixedFieldSettingsBuilder(MemberInfo member)
        {
            _member = member ?? throw new ArgumentNullException(nameof(member));
        }

        /// <summary>
        /// Determines whether a field's contents should be truncated if it exceeds the configured length when writing to a file.
        /// </summary>
        public IFixedFieldSettingsBuilder TruncateFieldContentIfExceedLength()
        {
            _truncateIfExceedFieldLength = true;
            return this;
        }

        /// <summary>
        /// Defines a function that transforms the values of an object's members after they have been converted to a string, but
        /// before they have been written to a line in a file.
        /// </summary>
        /// <param name="stringNormalizer">The normalization function.</param>
        public IFixedFieldSettingsBuilder WithStringNormalizer(Func<string, string> stringNormalizer)
        {
            _stringNormalizer = stringNormalizer;
            return this;
        }

        /// <summary>
        /// Specifies the expected length of a field.
        /// </summary>
        /// <param name="length">The length of the field.</param>
        public IFixedFieldSettingsBuilder WithLength(int length)
        {
            _length = length;
            return this;
        }

        /// <summary>
        /// Specifies that a field begins with padding that should be removed.
        /// </summary>
        /// <param name="paddingChar">The padding character.</param>
        public IFixedFieldSettingsBuilder WithLeftPadding(char paddingChar)
        {
            _paddingChar = paddingChar;
            _padLeft = true;
            return this;
        }

        /// <summary>
        /// Specifies that a field ends with padding that should be removed.
        /// </summary>
        /// <param name="paddingChar">The padding character.</param>
        public IFixedFieldSettingsBuilder WithRightPadding(char paddingChar)
        {
            _paddingChar = paddingChar;
            _padLeft = false;
            return this;
        }

        /// <summary>
        /// Provides a condition that, when true, indicates that the character at the given index
        /// should not be included in the field's value. Application of the predicate will stop at
        /// the first false evaluation.
        /// </summary>
        /// <param name="predicate">The predicate to apply to each character.</param>
        public IFixedFieldSettingsBuilder SkipWhile(Func<char, int, bool> predicate)
        {
            _skipWhile = predicate ?? throw new ArgumentNullException(nameof(predicate));
            return this;
        }

        /// <summary>
        /// Provides a condition that, when true, indicates that the character at the given index
        /// should be included in the field's value. Application of the predicate will stop at
        /// the first true evaluation.
        /// </summary>
        /// <param name="predicate">The predicate to apply to each character.</param>
        public IFixedFieldSettingsBuilder TakeUntil(Func<char, int, bool> predicate)
        {
            _takeUntil = predicate ?? throw new ArgumentNullException(nameof(predicate));
            return this;
        }

        /// <summary>
        /// Specifies that a field can be null and provides the string value that indicates null data.
        /// </summary>
        /// <param name="nullValue">The string that indicates a null valued field.</param>
        public IFixedFieldSettingsBuilder AllowNull(string nullValue)
        {
            _isNullable = true;
            _nullValue = nullValue;
            return this;
        }

        /// <summary>
        /// Specifies that a field's value should be converted using a new instance of the type <typeparamref name="TConverter"/>.
        /// </summary>
        /// <typeparam name="TConverter">The type of <see cref="IFieldValueConverter"/> to use for conversion.</typeparam>
        public IFixedFieldSettingsBuilder WithConverter<TConverter>() where TConverter : IFieldValueConverter, new()
        {
            return WithConverter(ReflectionHelper.CreateInstance<TConverter>(true));
        }

        /// <summary>
        ///  Specifies that a field's value should be converted using the provided <see cref="IFieldValueConverter"/> implementation.
        /// </summary>
        /// <param name="converter">The converter to use.</param>
        public IFixedFieldSettingsBuilder WithConverter(IFieldValueConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }

        /// <summary>
        /// Specifies that a field's value should be converted from a string to its destination type using the provided conversion function.
        /// </summary>
        /// <typeparam name="TProperty">The type of the destination property.</typeparam>
        /// <param name="parser">A lambda function converting from a string.</param>
        public IFixedFieldSettingsBuilder WithConversionFromString<TProperty>(FieldParser<TProperty> parser)
        {
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

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
        public IFixedFieldSettingsBuilder WithConversionToString<TProperty>(FieldFormatter<TProperty> formatter)
        {
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            if (_converter == null)
                _converter = new DelegatingConverter<TProperty>();

            if (_converter is DelegatingConverter<TProperty> delegatingConverter)
                delegatingConverter.FormatValue = formatter;
            else
                throw new InvalidOperationException("A converter has already been explicitly set.");

            return this;
        }

        /// <summary>
        /// Creates the field configuration.
        /// </summary>
        public IFixedFieldSettingsContainer Build()
        {
            return new FixedFieldSettings(_member)
            {
                IsNullable = _isNullable,
                NullValue = _nullValue,
                PaddingChar = _paddingChar,
                SkipWhile = _skipWhile,
                TakeUntil = _takeUntil,
                PadLeft = _padLeft,
                Length = _length,
                StringNormalizer = _stringNormalizer,
                TruncateIfExceedFieldLength = _truncateIfExceedFieldLength,
                Converter = _converter
            };
        }
    }
}