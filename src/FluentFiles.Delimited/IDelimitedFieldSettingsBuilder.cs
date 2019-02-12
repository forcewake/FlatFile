namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    /// <summary>
    /// Configures a delimited file field.
    /// </summary>
    public interface IDelimitedFieldSettingsBuilder : IFieldSettingsBuilder<IDelimitedFieldSettingsBuilder, IDelimitedFieldSettingsContainer>
    {
        /// <summary>
        /// Sets the name to use when writing a field's header.
        /// </summary>
        /// <param name="name">The field's header name.</param>
        IDelimitedFieldSettingsBuilder WithName(string name);
    }
}