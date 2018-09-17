using FluentFiles.Core.Base;
using FluentFiles.Core.Conversion;
using System.ComponentModel;

namespace FluentFiles.Core
{
    /// <summary>
    /// Provides extensions for <see cref="IFieldSettingsBuilder{TBuilder, TSettings}"/>.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Specifies that a field's value should be converted using the provided <see cref="TypeConverter"/>.
        /// </summary>
        /// <param name="builder">The settings builder.</param>
        /// <param name="typeConverter">The type converter to use.</param>
        public static TBuilder WithTypeConverter<TBuilder, TSettings>(this TBuilder builder, TypeConverter typeConverter)
            where TBuilder : IFieldSettingsBuilder<TBuilder, TSettings>
            where TSettings : IFieldSettings
        {
            return builder.WithConverter(new TypeConverterAdapter(typeConverter));
        }
    }
}
