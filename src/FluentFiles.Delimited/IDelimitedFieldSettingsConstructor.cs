namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    public interface IDelimitedFieldSettingsConstructor :
        IFieldSettingsConstructor<IDelimitedFieldSettingsConstructor>
    {
        string Name { get; }

        IDelimitedFieldSettingsConstructor WithName(string name);
    }
}