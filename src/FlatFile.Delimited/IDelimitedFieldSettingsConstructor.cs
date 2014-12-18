namespace FlatFile.Delimited
{
    using FlatFile.Core;
    using FlatFile.Delimited.Implementation;

    public interface IDelimitedFieldSettingsConstructor :
        IFieldSettingsConstructor<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor>
    {
    }
}