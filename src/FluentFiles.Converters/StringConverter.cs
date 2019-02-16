namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;

    /// <summary>
    /// Converts values that are already strings.
    /// </summary>
    public sealed class StringConverter : IFieldValueConverter
    {
        /// <summary>
        /// Returns true if both types are strings.
        /// </summary>
        public bool CanConvert(Type from, Type to) => from == typeof(string) && to == typeof(string);

        /// <summary>
        /// Converts a <see cref="ReadOnlySpan{Char}"/>s to a string.
        /// </summary>
        /// <param name="context">Provides information about a field deserialization operation.</param>
        /// <returns>A string.</returns>
        public object ConvertFromString(in FieldDeserializationContext context) => context.Source.Length == 0 ? string.Empty : context.Source.ToString();

        /// <summary>
        /// Performs a pass-through.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>The string that was passed in.</returns>
        public string ConvertToString(in FieldSerializationContext context) => (string)context.Source;
    }
}
