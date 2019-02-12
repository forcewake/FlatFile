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

    public interface IDelimitedFieldSettingsContainer : IDelimitedFieldSettings, IFieldSettingsContainer
    {
    }
}