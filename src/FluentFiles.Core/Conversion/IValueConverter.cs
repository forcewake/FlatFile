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
        /// Whether a value of a given type can be converted to another type.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <param name="to">The type t oconvert to.</param>
        /// <returns>Whether a conversion can be performed between the two types.</returns>
        bool CanConvert(Type from, Type to);

        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="source">The object to serialize.</param>
        /// <param name="sourceProperty">The property the object value is from.</param>
        /// <returns>A serialized version of <paramref name="source"/>.</returns>
        string ConvertToString(object source, PropertyInfo sourceProperty);

        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="targetProperty">The property the object value should be assigned to.</param>
        /// <returns>A deserialized version of <paramref name="source"/>.</returns>
        object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty);
    }
}