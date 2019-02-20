namespace FluentFiles.Delimited
{
    using FluentFiles.Core;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Provides extensions for <see cref="IDelimitedFieldSettingsBuilder"/>.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Specifies that a field's value should be converted using the provided <see cref="TypeConverter"/>.
        /// </summary>
        /// <param name="builder">The settings builder.</param>
        /// <param name="typeConverter">The type converter to use.</param>
        public static IDelimitedFieldSettingsBuilder WithTypeConverter(this IDelimitedFieldSettingsBuilder builder, TypeConverter typeConverter)
        {
            return builder.WithConverter(new TypeConverterAdapter(typeConverter));
        }

#pragma warning disable CS0618 // Type or member is obsolete
        /// <summary>
        /// Specifies that a field's value should be converted using a new instance of the type <typeparamref name="TConverter"/>.
        /// </summary>
        /// <typeparam name="TConverter">The type of <see cref="ITypeConverter"/> to use for conversion.</typeparam>
        public static IDelimitedFieldSettingsBuilder WithTypeConverter<TConverter>(this IDelimitedFieldSettingsBuilder builder) where TConverter : ITypeConverter, new()
        {
            return builder.WithTypeConverter(ReflectionHelper.CreateInstance<TConverter>(true));
        }

        /// <summary>
        ///  Specifies that a field's value should be converted using the provided <see cref="ITypeConverter"/> implementation.
        /// </summary>
        /// <param name="builder">The settings builder.</param>
        /// <param name="converter">The converter to use.</param>
        public static IDelimitedFieldSettingsBuilder WithTypeConverter(this IDelimitedFieldSettingsBuilder builder, ITypeConverter converter)
        {
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return builder.WithConverter(new ITypeConverterAdapter(converter));
        }
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
