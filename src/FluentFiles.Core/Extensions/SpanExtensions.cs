using System;

namespace FluentFiles.Core.Extensions
{
    /// <summary>
    /// Provides extension methods related to <see cref="Span{T}"/>, <see cref="ReadOnlySpan{T}"/>, <see cref="Memory{T}"/>, and <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    public static class SpanExtensions
    {
        /// <summary>
        /// Finds the first index at which a substring occurs using a starting index.
        /// </summary>
        /// <param name="span">The character string to search in.</param>
        /// <param name="value">The character string to search for.</param>
        /// <param name="startIndex">The index in <paramref name="span"/> from which to start searching.</param>
        /// <param name="comparisonType">The type of string comparison to use.</param>
        /// <returns>The first index in <paramref name="span"/> at which <paramref name="value"/> can be found, or -1 if it does not occur.</returns>
        public static int IndexOf(in this ReadOnlySpan<char> span, in ReadOnlySpan<char> value, int startIndex, StringComparison comparisonType)
        {
            var index = span.Slice(startIndex).IndexOf(value, comparisonType);
            if (index > -1)
                return index + startIndex;

            return index;
        }
    }
}
