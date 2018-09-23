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

        /// <summary>
        /// Splits a span using the given separators.
        /// Based on https://gist.github.com/LordJZ/92b7decebe52178a445a0b82f63e585a.
        /// </summary>
        /// <typeparam name="T">The type of items to split.</typeparam>
        /// <param name="span">The span to split.</param>
        /// <param name="separators">The items to split by.</param>
        /// <returns>An enumerable over the split span segments.</returns>
        public static SplitEnumerable<T> Split<T>(this ReadOnlySpan<T> span, params T[] separators) where T : IEquatable<T> =>
            span.Split(new ReadOnlySpan<T>(separators));

        /// <summary>
        /// Splits a span using the given separators.
        /// Based on https://gist.github.com/LordJZ/92b7decebe52178a445a0b82f63e585a.
        /// </summary>
        /// <typeparam name="T">The type of items to split.</typeparam>
        /// <param name="span">The span to split.</param>
        /// <param name="separators">The items to split by.</param>
        /// <returns>An enumerable over the split span segments.</returns>
        public static SplitEnumerable<T> Split<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separators) where T : IEquatable<T> => 
            new SplitEnumerable<T>(span, separators);

        public readonly ref struct SplitEnumerable<T> where T : IEquatable<T>
        {
            public SplitEnumerable(ReadOnlySpan<T> span, ReadOnlySpan<T> separators)
            {
                Span = span;
                Separators = separators;
            }

            ReadOnlySpan<T> Span { get; }
            ReadOnlySpan<T> Separators { get; }

            public SplitEnumerator<T> GetEnumerator() => new SplitEnumerator<T>(Span, Separators);
        }

        public ref struct SplitEnumerator<T> where T : IEquatable<T>
        {
            public SplitEnumerator(ReadOnlySpan<T> span, ReadOnlySpan<T> separators)
            {
                Span = span;
                Separators = separators;
                Current = default;
                TrailingEmptyItem = Span.IsEmpty;
            }

            ReadOnlySpan<T> Span { get; set; }
            ReadOnlySpan<T> Separators { get; }
            int SeparatorLength => 1;
            bool TrailingEmptyItem { get; set; }

            public bool MoveNext()
            {
                if (TrailingEmptyItem)
                {
                    TrailingEmptyItem = false;
                    Current = default;
                    return true;
                }

                if (Span.IsEmpty)
                {
                    Span = Current = default;
                    return false;
                }

                int index = Span.IndexOfAny(Separators);
                if (index < 0)
                {
                    Current = Span;
                    Span = default;
                }
                else
                {
                    Current = Span.Slice(0, index);
                    Span = Span.Slice(index + SeparatorLength);
                    if (Span.IsEmpty)
                        TrailingEmptyItem = true;
                }

                return true;
            }

            public ReadOnlySpan<T> Current { get; private set; }
        }
    }
}
