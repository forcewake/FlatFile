namespace FluentFiles.FixedLength
{
    /// <summary>
    /// Provides extensions for <see cref="IFixedFieldSettingsBuilder"/>.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Specifies an index at which a value actually begins within a field.
        /// </summary>
        public static IFixedFieldSettingsBuilder StartsAt(this IFixedFieldSettingsBuilder builder, ushort startIndex)
        {
            return builder.SkipWhile((_, i) => i < startIndex);
        }

        /// <summary>
        /// Specifies an index at which a value actually ends within a field.
        /// </summary>
        public static IFixedFieldSettingsBuilder EndsAt(this IFixedFieldSettingsBuilder builder, ushort endIndex)
        {
            return builder.TakeUntil((_, i) => i > endIndex);
        }
    }
}
