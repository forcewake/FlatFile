namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedFieldSettingsConstructor :
        IFieldSettingsConstructor<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor>
    {
        string Name { get; }

        IDelimitedFieldSettingsConstructor WithName(string name);
    }
}