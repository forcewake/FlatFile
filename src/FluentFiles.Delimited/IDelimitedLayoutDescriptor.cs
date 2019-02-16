namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    /// <summary>
    /// Describes the mapping from a delimited file record to a target type.
    /// </summary>
    public interface IDelimitedLayoutDescriptor : ILayoutDescriptor<IDelimitedFieldSettingsContainer>
    {
        /// <summary>
        /// The delimiter used to separate fields in a record.
        /// </summary>
        string Delimiter { get; }

        /// <summary>
        /// The string used to quote delimited fields.
        /// </summary>
        string Quotes { get; }
    }
}