namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;

    /// <summary>
    /// Converts between GUID values and strings.
    /// </summary>
    public sealed class GuidConverter : ConverterBase<Guid>
    {
        /// <summary>
        /// Converts a string to a GUID.
        /// </summary>
        /// <param name="context">Provides information about a field parsing operation.</param>
        /// <returns>A parsed GUID.</returns>
        protected override Guid ParseValue(in FieldParsingContext context) =>
            Guid.Parse(context.Source.Trim());

        /// <summary>
        /// Converts a GUID to a string.
        /// </summary>
        /// <param name="context">Provides information about a field formatting operation.</param>
        /// <returns>A formatted GUID.</returns>
        protected override string FormatValue(in FieldFormattingContext<Guid> context) =>
            context.Source.ToString();
    }
}
