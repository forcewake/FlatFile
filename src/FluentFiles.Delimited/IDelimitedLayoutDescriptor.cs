namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLayoutDescriptor : ILayoutDescriptor<IDelimitedFieldSettingsContainer>
    {
        string Delimiter { get; }
        string Quotes { get; }
    }
}