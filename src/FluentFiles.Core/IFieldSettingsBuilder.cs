namespace FluentFiles.Core
{
    using FluentFiles.Core.Base;
    using System;

    public interface IFieldSettingsBuilder<out TBuilder, out TSettings>
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
        /// <typeparam name="TConverter">The type of <see cref="ITypeConverter"/> to use for conversion.</typeparam>
        TBuilder WithTypeConverter<TConverter>() where TConverter : ITypeConverter, new();

        /// <summary>
        ///  Specifies that a field's value should be converted using the provided <see cref="ITypeConverter"/> implementation.
        /// </summary>
        /// <param name="converter">The converter to use.</param>
        TBuilder WithTypeConverter(ITypeConverter converter);

        /// <summary>
        /// Specifies that a field's value should be converted from a string to its destination type using the provided conversion function.
        /// </summary>
        /// <typeparam name="TProperty">The type of the destination property.</typeparam>
        /// <param name="conversion">A lambda function converting from a string.</param>
        TBuilder WithConversionFromString<TProperty>(Func<string, TProperty> conversion);

        /// <summary>
        /// Specifies that a field's value should be converted to a string from its destination type using the provided conversion function.
        /// </summary>
        /// <typeparam name="TProperty">The type of the source property.</typeparam>
        /// <param name="conversion">A lambda function converting to a string.</param>
        TBuilder WithConversionToString<TProperty>(Func<TProperty, string> conversion);

        /// <summary>
        /// Constructs a field's settings.
        /// </summary>
        TSettings Build();
    }
}