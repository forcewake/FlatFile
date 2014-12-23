namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLengthFileEngine<T> :
       IFlatFileEngine<T, IFixedLayout<T>, FixedFieldSettings, IFixedFieldSettingsConstructor>
       where T : class, new()
    {
    }
}