namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLayout<TTarget> :
        IDelimitedLayoutDescriptor,
        ILayout<TTarget, IDelimitedFieldSettingsContainer, IDelimitedFieldSettingsConstructor, IDelimitedLayout<TTarget>>
    {
        IDelimitedLayout<TTarget> WithQuote(string quote);
        IDelimitedLayout<TTarget> WithDelimiter(string delimiter);
    }
}