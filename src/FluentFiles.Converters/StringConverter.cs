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
        /// Whether a value of a given type can be converted from a string.
        /// </summary>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        public bool CanParse(Type to) => to == typeof(string);

        /// <summary>
        /// Whether a value of a given type can be converted to a string.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        public bool CanFormat(Type from) => from == typeof(string);

        /// <summary>
        /// Converts a <see cref="ReadOnlySpan{Char}"/>s to a string.
        /// </summary>
        /// <param name="context">Provides information about a field parsing operation.</param>
        /// <returns>A string.</returns>
        public object Parse(in FieldParsingContext context) => context.Source.Length == 0 ? string.Empty : context.Source.ToString();

        /// <summary>
        /// Performs a pass-through.
        /// </summary>
        /// <param name="context">Provides information about a field formatting operation.</param>
        /// <returns>The string that was passed in.</returns>
        public string Format(in FieldFormattingContext context) => (string)context.Source;
    }
}
