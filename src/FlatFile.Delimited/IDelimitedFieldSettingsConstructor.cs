namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedFieldSettingsConstructor :
        IFieldSettingsConstructor<IDelimitedFieldSettingsConstructor>
    {
        string Name { get; }

        IDelimitedFieldSettingsConstructor WithName(string name);
    }
}