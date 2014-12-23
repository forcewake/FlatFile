namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLayout<TTarget> :
        ILayout<TTarget, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor, IDelimitedLayout<TTarget>>
    {
        string Delimiter { get; }
        string Quotes { get; }

        IDelimitedLayout<TTarget> WithQuote(string quote);
        IDelimitedLayout<TTarget> WithDelimiter(string delimiter);
    }
}