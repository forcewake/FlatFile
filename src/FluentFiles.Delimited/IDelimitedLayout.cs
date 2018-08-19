namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    public interface IDelimitedLayout<TTarget> :
        IDelimitedLayoutDescriptor,
        ILayout<TTarget, IDelimitedFieldSettingsContainer, IDelimitedFieldSettingsConstructor, IDelimitedLayout<TTarget>>
    {
        IDelimitedLayout<TTarget> WithQuote(string quote);
        IDelimitedLayout<TTarget> WithDelimiter(string delimiter);
    }
}