namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    public interface IDelimitedFieldSettingsBuilder : IFieldSettingsBuilder<IDelimitedFieldSettingsBuilder, IDelimitedFieldSettingsContainer>
    {
        IDelimitedFieldSettingsBuilder WithName(string name);
    }
}