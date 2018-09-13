using System;
using System.Reflection;

namespace FluentFiles.Core.Conversion
{
    /// <summary>
    /// Defines a way to convert a field's string value to and from a custom type.
    /// </summary>
    public interface IValueConverter
    {
        /// <summary>
        /// Whether a string can be deserialized to a given type.
        /// </summary>
        /// <param name="type">The type to deserialize.</param>
        bool CanConvertFrom(Type type);

        /// <summary>
        /// Whether a given type can be serialized to a string.
        /// </summary>
        /// <param name="type">The type to serialize.</param>
        bool CanConvertTo(Type type);

        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="source">The object to serialize.</param>
        /// <param name="sourceProperty">The property the object value is from.</param>
        /// <returns>A serialized version of <paramref name="source"/>.</returns>
        ReadOnlySpan<char> ConvertToString(object source, PropertyInfo sourceProperty);

        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="targetProperty">The property the object value should be assigned to.</param>
        /// <returns>A deserialized version of <paramref name="source"/>.</returns>
        object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty);
    }
}