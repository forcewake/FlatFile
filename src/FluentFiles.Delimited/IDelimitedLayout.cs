namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    /// <summary>
    /// Describes the mapping from a delimited file record to a target type.
    /// </summary>
    /// <typeparam name="TTarget">The type that a record maps to.</typeparam>
    public interface IDelimitedLayout<TTarget> :
        IDelimitedLayoutDescriptor,
        ILayout<TTarget, IDelimitedFieldSettingsContainer, IDelimitedFieldSettingsBuilder, IDelimitedLayout<TTarget>>
    {
        /// <summary>
        /// Specifies the string used to quote delimited fields.
        /// </summary>
        /// <param name="quote">The quote string.</param>
        IDelimitedLayout<TTarget> WithQuote(string quote);

        /// <summary>
        /// Specifies the delimiter used to separate fields in a record.
        /// </summary>
        /// <param name="delimiter">The delimiter string.</param>
        IDelimitedLayout<TTarget> WithDelimiter(string delimiter);
    }
}