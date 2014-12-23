namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedFileEngine<T> :
        IFlatFileEngine<T, IDelimitedLayout<T>, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor>
        where T : class, new()
    {
    }
}