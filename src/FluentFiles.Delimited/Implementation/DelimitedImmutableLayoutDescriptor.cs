namespace FluentFiles.Delimited.Implementation
{
    using FluentFiles.Core.Base;

    /// <summary>
    /// A delimited file layout descriptor that has been finalized and cannot be changed.
    /// </summary>
    public sealed class DelimitedImmutableLayoutDescriptor : ImmutableLayoutDescriptor<IDelimitedFieldSettingsContainer>, IDelimitedLayoutDescriptor
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DelimitedImmutableLayoutDescriptor"/>.
        /// </summary>
        /// <param name="existing">The descriptor to copy.</param>
        public DelimitedImmutableLayoutDescriptor(IDelimitedLayoutDescriptor existing)
            : base(existing)
        {
            Delimiter = existing.Delimiter;
            Quotes = existing.Quotes;
        }

        /// <summary>
        /// The delimiter used to separate fields in a record.
        /// </summary>
        public string Delimiter { get; }

        /// <summary>
        /// The string used to quote delimited fields.
        /// </summary>
        public string Quotes { get; }
    }
}
