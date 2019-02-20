namespace FluentFiles.Core
{
    using FluentFiles.Core.Conversion;
    using System;

    /// <summary>
    /// A builder that can configure and produce a mapping configuration for a fixed-length field.
    /// </summary>
    /// <typeparam name="TBuilder">The self-referencing type of a builder.</typeparam>
    /// <typeparam name="TSettings">The type of field mapping being configured.</typeparam>
    public interface IFieldSettingsBuilder<out TBuilder, out TSettings> : IBuildable<TSettings>
        where TBuilder : IFieldSettingsBuilder<TBuilder, TSettings>
        where TSettings : IFieldSettings
    {
        /// <summary>
        /// Specifies that a field can be null and provides the string value that indicates null data.
        /// </summary>
        /// <param name="nullValue">The string that indicates a null valued field.</param>
        TBuilder AllowNull(string nullValue);

        /// <summary>
        /// Specifies that a field's value should be converted using a new instance of the type <typeparamref name="TConverter"/>.
        /// </summary>
        /// <typeparam name="TConverter">The type of <see cref="IFieldValueConverter"/> to use for conversion.</typeparam>
        TBuilder WithConverter<TConverter>() where TConverter : IFieldValueConverter, new();

        /// <summary>
        ///  Specifies that a field's value should be converted using the provided <see cref="IFieldValueConverter"/> implementation.
        /// </summary>
        /// <param name="converter">The converter to use.</param>
        TBuilder WithConverter(IFieldValueConverter converter);

        /// <summary>
        /// Specifies that a field's value should be converted from a string to its destination type using the provided conversion function.
        /// </summary>
        /// <typeparam name="TProperty">The type of the destination property.</typeparam>
        /// <param name="parser">A lambda function converting from a string.</param>
        TBuilder WithConversionFromString<TProperty>(FieldParser<TProperty> parser);

        /// <summary>
        /// Specifies that a field's value should be converted to a string from its source type using the provided conversion function.
        /// </summary>
        /// <typeparam name="TProperty">The type of the source property.</typeparam>
        /// <param name="formatter">A lambda function converting to a string.</param>
        TBuilder WithConversionToString<TProperty>(FieldFormatter<TProperty> formatter);
    }

    /// <summary>
    /// A function that converts from a string to a value.
    /// </summary>
    /// <typeparam name="TResult">The type of value to convert to.</typeparam>
    /// <param name="source">The string to convert from.</param>
    /// <returns>A <typeparamref name="TResult"/> value.</returns>
    public delegate TResult FieldParser<out TResult>(ReadOnlySpan<char> source);

    /// <summary>
    /// A function that converts from a value to a string.
    /// </summary>
    /// <typeparam name="TValue">The type of value to convert from.</typeparam>
    /// <param name="source">The value to convert from.</param>
    /// <returns>A string representation of <typeparamref name="TValue"/>.</returns>
    public delegate string FieldFormatter<in TValue>(TValue source);
}