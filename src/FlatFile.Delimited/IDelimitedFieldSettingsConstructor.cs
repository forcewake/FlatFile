namespace FlatFile.Delimited
{
    using FlatFile.Core;
    using FlatFile.Delimited.Implementation;

    public interface IDelimitedFieldSettingsConstructor :
        IFieldSettingsConstructor<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor>
    {
        string Name { get; }

        IDelimitedFieldSettingsConstructor WithName(string name);
    }
}