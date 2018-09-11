namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    public interface IDelimitedLayout<TTarget> :
        IDelimitedLayoutDescriptor,
        ILayout<TTarget, IDelimitedFieldSettingsContainer, IDelimitedFieldSettingsBuilder, IDelimitedLayout<TTarget>>
    {
        IDelimitedLayout<TTarget> WithQuote(string quote);
        IDelimitedLayout<TTarget> WithDelimiter(string delimiter);
    }
}