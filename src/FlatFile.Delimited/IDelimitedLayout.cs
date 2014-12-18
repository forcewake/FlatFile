namespace FlatFile.Delimited
{
    using FlatFile.Core;
    using FlatFile.Delimited.Implementation;

    public interface IDelimitedLayout<T> : ILayout<T, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor, IDelimitedLayout<T>>
    {
        string Delimiter { get; }
        string Quotes { get; }
    }
}