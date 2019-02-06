namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    public interface IDelimitedFieldSettings : IFieldSettings
    {
        string Name { get; set; }
    }

    public interface IDelimitedFieldSettingsContainer : IDelimitedFieldSettings, IFieldSettingsContainer
    {
    }
}