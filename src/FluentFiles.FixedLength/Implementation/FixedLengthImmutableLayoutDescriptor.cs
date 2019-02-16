namespace FluentFiles.FixedLength.Implementation
{
    using FluentFiles.Core.Base;

    /// <summary>
    /// A fixed-length file layout descriptor that has been finalized and cannot be changed.
    /// </summary>
    public sealed class FixedLengthImmutableLayoutDescriptor : ImmutableLayoutDescriptor<IFixedFieldSettingsContainer>, IFixedLengthLayoutDescriptor
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FixedLengthImmutableLayoutDescriptor"/>.
        /// </summary>
        /// <param name="existing">The descriptor to copy.</param>
        public FixedLengthImmutableLayoutDescriptor(IFixedLengthLayoutDescriptor existing)
            : base(existing)
        {
        }
    }
}
