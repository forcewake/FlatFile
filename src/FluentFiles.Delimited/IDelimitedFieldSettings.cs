namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    /// <summary>
    /// A delimited field mapping configuration.
    /// </summary>
    public interface IDelimitedFieldSettings : IFieldSettings
    {
        /// <summary>
        /// The name to use when writing a field's header.
        /// </summary>
        string Name { get; set; }
    }

    /// <summary>
    /// Extends <see cref="IDelimitedFieldSettings"/> with functionality and data related to its storage in a class property.
    /// See <see cref="IFieldSettingsContainer"/>.
    /// </summary>
    public interface IDelimitedFieldSettingsContainer : IDelimitedFieldSettings, IFieldSettingsContainer
    {
    }
}