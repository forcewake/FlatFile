namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    public interface IDelimitedLayoutDescriptor : ILayoutDescriptor<IDelimitedFieldSettingsContainer>
    {
        string Delimiter { get; }
        string Quotes { get; }
    }
}