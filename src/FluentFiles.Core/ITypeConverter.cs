namespace FluentFiles.Core
{
    using FluentFiles.Core.Conversion;
    using System;

    /// <summary>
    /// A deprecated interface for use in converting field values.
    /// Please use <see cref="IFieldValueConverter"/> instead.
    /// </summary>
    [Obsolete("Please implement IFieldValueConverter instead.", false)]
    public interface ITypeConverter
    {
        /// <summary>
        /// Whether a converter can convert from a given type.
        /// </summary>
        /// <param name="type">The type to convert from.</param>
        /// <returns>Whether a conversion can be performed.</returns>
        bool CanConvertFrom(Type type);

        /// <summary>
        /// Whether a converter can convert to a given type.
        /// </summary>
        /// <param name="type">The type to convert to.</param>
        /// <returns>Whether a conversion can be performed.</returns>
        bool CanConvertTo(Type type);

        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <returns>A converted string.</returns>
        string ConvertToString(object source);

        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="source">The string to convert.</param>
        /// <returns>A converted value.</returns>
        object ConvertFromString(string source);
    }
}