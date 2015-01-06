namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLayoutDescriptor : ILayoutDescriptor<DelimitedFieldSettings>
    {
        string Delimiter { get; }
        string Quotes { get; }
    }
}