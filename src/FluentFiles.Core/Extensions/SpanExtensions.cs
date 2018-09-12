using System;

namespace FluentFiles.Core.Extensions
{
    public static class SpanExtensions
    {
        public static int IndexOf(in this ReadOnlySpan<char> span, in ReadOnlySpan<char> value, int startIndex, StringComparison comparisonType)
        {
            var index = span.Slice(startIndex).IndexOf(value, comparisonType);
            if (index > -1)
                return index + startIndex;

            return index;
        }
    }
}
